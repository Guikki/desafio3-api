# Desafio 3 API

Este projeto consiste no desenvolvimento de uma API REST completa utilizando ASP.NET Core, Entity Framework Core, SQLite e autenticação JWT, acompanhada de um frontend simples desenvolvido em HTML, CSS e JavaScript.

A proposta principal do sistema é permitir o gerenciamento de usuários e tarefas, incluindo autenticação, autorização e operações completas de CRUD.

O projeto foi estruturado utilizando separação em camadas, buscando maior organização, legibilidade e facilidade de manutenção do código.

---

# Tecnologias Utilizadas

No backend foram utilizadas as seguintes tecnologias:

- ASP.NET Core 10
- Entity Framework Core
- SQLite
- JWT Bearer Authentication
- Swagger / Swashbuckle

No frontend foram utilizados:

- HTML5
- CSS3
- JavaScript
- Bootstrap 5

---

# Estrutura do Projeto

O sistema foi dividido em camadas separadas, organizadas da seguinte forma:

```text
Desafio3API
│
├── Controllers
├── Data
├── DTOs
├── Models
├── Repositories
├── Services
├── Migrations
├── Frontend
│   ├── index.html
│   ├── register.html
│   ├── dashboard.html
│   ├── style.css
│   └── script.js
│
├── Program.cs
├── appsettings.json
└── README.md
```

A camada de Controllers ficou responsável pelos endpoints da API.

A camada de Services concentra as regras de negócio.

Os Repositories realizam o acesso ao banco de dados.

Os DTOs foram utilizados para controle de entrada e saída de dados, evitando exposição direta das entidades.

---

# Funcionalidades Desenvolvidas

O sistema permite:

- cadastro de usuários;
- autenticação com JWT;
- listagem de usuários;
- atualização de usuários;
- exclusão de usuários;
- criação de tarefas;
- listagem de tarefas;
- atualização de tarefas;
- conclusão e reabertura de tarefas;
- exclusão de tarefas.

As rotas protegidas exigem autenticação via token JWT.

---

# Autenticação e Segurança

A autenticação foi implementada utilizando JWT Bearer Token.

Após realizar o login, o sistema gera um token que deve ser enviado nas requisições protegidas através do cabeçalho:

```http
Authorization: Bearer TOKEN
```

O Swagger também foi configurado para trabalhar com autenticação JWT, permitindo testar os endpoints protegidos diretamente pela interface gráfica.

---

# Swagger

O Swagger foi configurado para documentação e testes da API.

Após executar a aplicação, o Swagger pode ser acessado em:

```text
http://localhost:5055/swagger
```

Os endpoints protegidos apresentam cadeado de autenticação, sendo necessário utilizar o botão "Authorize" para inserir o token JWT.

---

# Frontend

O frontend foi desenvolvido de forma simples e objetiva, utilizando HTML, CSS e JavaScript puro.

O fluxo implementado possui:

- tela de login;
- tela de cadastro;
- dashboard de tarefas;
- integração completa com a API.

A comunicação ocorre utilizando requisições HTTP através de fetch.

---

# Como Executar o Projeto

Primeiramente, é necessário clonar o repositório:

```bash
git clone URL_DO_REPOSITORIO
```

Depois, entrar na pasta do projeto:

```bash
cd Desafio3API
```

Em seguida, restaurar as dependências:

```bash
dotnet restore
```

Aplicar as migrations:

```bash
dotnet ef database update
```

Por fim, executar a aplicação:

```bash
dotnet run
```

---

# Endereços da Aplicação

API:

```text
http://localhost:5055
```

Swagger:

```text
http://localhost:5055/swagger
```

Frontend:

Abrir o arquivo:

```text
Frontend/index.html
```

---

# Validações Implementadas

O sistema possui validações para:

- campos obrigatórios;
- email válido;
- senha mínima;
- autenticação JWT;
- autorização de endpoints protegidos.

---

# Considerações Finais

O projeto foi desenvolvido buscando aplicar conceitos importantes de desenvolvimento backend com ASP.NET Core, incluindo organização em camadas, autenticação, integração com banco de dados relacional e consumo da API pelo frontend.

Além da implementação das funcionalidades principais, também foi realizada a integração completa entre frontend e backend, permitindo uma experiência funcional de utilização do sistema.

---

# Desenvolvedor

Projeto desenvolvido por Guilherme como parte do Desafio 3 - API.