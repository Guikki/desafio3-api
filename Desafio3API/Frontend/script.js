const API_URL = "http://localhost:5055";

function getToken() {
    return localStorage.getItem("token");
}

function setMessage(message, type = "error") {
    const existingAlert = document.getElementById("customAlert");

    if (existingAlert) {
        existingAlert.remove();
    }

    let icon = "!";
    let iconClass = "alert-error";

    if (type === "success") {
        icon = "✓";
        iconClass = "alert-success";
    }

    const alertHtml = `
        <div id="customAlert" class="custom-alert-overlay">
            <div class="custom-alert-modal">
                <div class="custom-alert-icon ${iconClass}">
                    ${icon}
                </div>

                <p class="custom-alert-message">
                    ${message}
                </p>

                <button
                    class="custom-alert-button"
                    onclick="document.getElementById('customAlert').remove()"
                >
                    OK
                </button>
            </div>
        </div>
    `;

    document.body.insertAdjacentHTML("beforeend", alertHtml);
}

async function getErrorMessage(response) {
    const text = await response.text();

    try {
        const errorData = JSON.parse(text);

        if (errorData.errors) {
            return Object.values(errorData.errors)[0][0];
        }

        if (errorData.title) {
            return errorData.title;
        }

        return "Erro na requisição.";
    } catch {
        return text || "Erro na requisição.";
    }
}

function goToRegister() {
    window.location.href = "register.html";
}

function goToLogin() {
    window.location.href = "index.html";
}

function goToDashboard() {
    window.location.href = "dashboard.html";
}

async function register() {
    const name = document.getElementById("registerName").value;
    const email = document.getElementById("registerEmail").value;
    const password = document.getElementById("registerPassword").value;

    const response = await fetch(`${API_URL}/api/user`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            name,
            email,
            password
        })
    });

    if (response.ok) {
        setMessage("Usuário cadastrado com sucesso!", "success");

        setTimeout(() => {
            goToLogin();
        }, 1000);
    } else {
        const message = await getErrorMessage(response);

        setMessage(message);
    }
}

async function login() {
    const email = document.getElementById("loginEmail").value;
    const password = document.getElementById("loginPassword").value;

    const response = await fetch(`${API_URL}/api/auth/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            email,
            password
        })
    });

    if (response.ok) {
        const data = await response.json();

        localStorage.setItem("token", data.token);
        localStorage.setItem("userName", data.name);

        goToDashboard();
    } else {
        const message = await getErrorMessage(response);

        setMessage(message || "Email ou senha inválidos.");
    }
}

async function loadTasks() {
    const taskList = document.getElementById("taskList");

    if (!taskList) {
        return;
    }

    const response = await fetch(`${API_URL}/api/task`, {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${getToken()}`
        }
    });

    if (!response.ok) {
        return;
    }

    const tasks = await response.json();

    taskList.innerHTML = "";

    tasks.forEach(task => {
        const li = document.createElement("li");

        li.className = "list-group-item";

        if (task.isCompleted) {
            li.classList.add("completed-task");
        }

        li.innerHTML = `
            <span>
                <strong>${task.title}</strong> - ${task.description}
            </span>

            <div class="task-buttons">
                <button class="btn btn-sm btn-warning"
                    onclick="toggleTask(${task.id}, '${task.title}', '${task.description}', ${task.isCompleted})">
                    ${task.isCompleted ? "Reabrir" : "Concluir"}
                </button>

                <button class="btn btn-sm btn-danger"
                    onclick="deleteTask(${task.id})">
                    Excluir
                </button>
            </div>
        `;

        taskList.appendChild(li);
    });
}

async function createTask() {
    const title = document.getElementById("taskTitle").value;
    const description = document.getElementById("taskDescription").value;

    const response = await fetch(`${API_URL}/api/task`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${getToken()}`
        },
        body: JSON.stringify({
            title,
            description,
            isCompleted: false
        })
    });

    if (response.ok) {
        document.getElementById("taskTitle").value = "";
        document.getElementById("taskDescription").value = "";

        await loadTasks();
    } else {
        const message = await getErrorMessage(response);

        setMessage(message);
    }
}

async function toggleTask(id, title, description, isCompleted) {
    const response = await fetch(`${API_URL}/api/task/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${getToken()}`
        },
        body: JSON.stringify({
            title,
            description,
            isCompleted: !isCompleted
        })
    });

    if (response.ok) {
        await loadTasks();
    } else {
        const message = await getErrorMessage(response);

        setMessage(message);
    }
}

async function deleteTask(id) {
    const response = await fetch(`${API_URL}/api/task/${id}`, {
        method: "DELETE",
        headers: {
            "Authorization": `Bearer ${getToken()}`
        }
    });

    if (response.ok) {
        await loadTasks();
    } else {
        const message = await getErrorMessage(response);

        setMessage(message);
    }
}

function logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("userName");

    goToLogin();
}

window.onload = async () => {
    const welcomeMessage = document.getElementById("welcomeMessage");

    if (welcomeMessage) {
        const userName = localStorage.getItem("userName");

        welcomeMessage.innerText = `Bem-vindo, ${userName}!`;

        await loadTasks();
    }
};