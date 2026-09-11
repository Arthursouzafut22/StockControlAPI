<div align="center">

# 📦 Stock Management API

<p>
  API RESTful para gerenciamento de estoque e fluxo de mercadorias, desenvolvida em <strong>ASP.NET Core</strong> e <strong>Entity Framework</strong>.
</p>

<p>
  <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET"/>
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#"/>
  <img src="https://img.shields.io/badge/Entity%20Framework-6DB33F?style=for-the-badge&logo=nuget&logoColor=white" alt="Entity Framework"/>
  <img src="https://img.shields.io/badge/PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white" alt="PostgreSQL"/>
  <img src="https://img.shields.io/badge/Supabase-3FCF8E?style=for-the-badge&logo=supabase&logoColor=white" alt="Supabase"/>
  <img src="https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black" alt="Swagger"/>
</p>

</div>

---

## 💻 Projeto

Repositório de uma **WebAPI** para controle de estoque e fluxo de mercadorias. A plataforma permite gerenciar produtos, registrar entradas e saídas de estoque, acompanhar movimentações, autenticar usuários e gerar relatórios financeiros — tudo através de uma API RESTful documentada com Swagger.

## ✨ Funcionalidades

- ✅ Cadastro, consulta, atualização e exclusão de produtos
- ✅ Registro de entradas e saídas de estoque
- ✅ Histórico e rastreamento de movimentações
- ✅ Autenticação de usuários
- ✅ Geração de relatórios financeiros (resumo, estoque e exportação em PDF)
- ✅ Documentação interativa via Swagger

## 🚀 Recursos Utilizados

- `ASP.NET Core`
- `C#`
- `Entity Framework Core`
- `PostgreSQL`
- `Supabase`
- `Swagger`

## 🗺️ Endpoints

### Produtos

| Método | Rota                | Descrição                     |
|--------|---------------------|--------------------------------|
| GET    | `/v1/produtos`      | Lista todos os produtos        |
| POST   | `/v1/produtos`      | Cadastra um novo produto       |
| GET    | `/v1/produtos/{id}` | Consulta um produto específico |
| PUT    | `/v1/produtos/{id}` | Atualiza um produto            |
| DELETE | `/v1/produtos/{id}` | Remove um produto              |

### Movimentações

| Método | Rota                                      | Descrição                          |
|--------|--------------------------------------------|-------------------------------------|
| GET    | `/v1/movimentacoes`                        | Lista todas as movimentações        |
| GET    | `/v1/movimentacoes/entrada`                | Lista as entradas de estoque        |
| POST   | `/v1/movimentacoes/entradas`               | Registra uma entrada de estoque     |
| GET    | `/v1/movimentacoes/saida`                  | Lista as saídas de estoque          |
| POST   | `/v1/movimentacoes/saidas`                 | Registra uma saída de estoque       |
| GET    | `/v1/movimentacoes/{id}`                   | Consulta uma movimentação           |
| PUT    | `/v1/movimentacoes/{id}/{productId}`       | Atualiza a movimentação de um produto |

### Relatórios

| Método | Rota                            | Descrição                            |
|--------|----------------------------------|----------------------------------------|
| GET    | `/v1/relatorios/resumo`          | Retorna um resumo financeiro/geral    |
| GET    | `/v1/relatorios/estoque`         | Retorna o relatório de estoque atual  |
| GET    | `/v1/relatorios/exportar-pdf`    | Exporta o relatório em PDF            |

## 📸 Screenshot

<div align="center">
  <img width="1892" height="896" alt="image" src="https://github.com/user-attachments/assets/9dc27a8c-f1a4-4170-9e10-aff3155514c9" />
</div>

## 👤 Autor

**Arthur Souza**

<p>
  <a href="https://github.com/arthursouza"><img src="https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white"/></a>
  <a href="https://linkedin.com/in/arthursouza"><img src="https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white"/></a>
</p>

---

<div align="center">
  Feito com 💜 por Arthur Souza
</div>
