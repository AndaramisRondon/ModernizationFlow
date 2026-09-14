# Modernization Flow

[English Version](README.md)

## Sobre o Autor

**Andaramis Bezerra**  
Desenvolvedor de Software Sênior / Arquiteto de Software

Profissional de desenvolvimento de software com ampla experiência em sistemas corporativos, com foco em C#/.NET, ASP.NET Core, ASP.NET MVC, APIs REST, SQL Server, arquitetura de aplicações e modernização de sistemas legados.

Este projeto demonstra uma estratégia de modernização incremental, mostrando como uma aplicação .NET existente pode evoluir para um frontend em React sem exigir uma reescrita completa do sistema.

### Contato

- **E-mail:** [andaramis@gmail.com](mailto:andaramis@gmail.com)
- **LinkedIn:** [linkedin.com/in/andaramis](https://www.linkedin.com/in/andaramis/)

---


O Modernization Flow é um projeto de referência que demonstra uma estratégia de **modernização incremental de aplicações legadas ASP.NET MVC**, migrando gradualmente a camada de apresentação para React sem descartar as regras de negócio e capacidades já existentes no backend.

O projeto simula um cenário comum em ambientes corporativos:

```text
MVC Legado
    ↓
Application / Casos de Uso
    ↓
ASP.NET Core REST API
    ↓
React
```

Em vez de reescrever toda a aplicação de uma única vez, a arquitetura permite que as funcionalidades sejam migradas gradualmente.

---

## Objetivos do Projeto

Os principais objetivos deste projeto são:

- Demonstrar a modernização incremental de uma aplicação MVC legada.
- Separar as regras de negócio da camada de apresentação.
- Expor as funcionalidades da aplicação através de APIs REST.
- Introduzir React sem exigir uma reescrita completa do backend.
- Manter as regras de workflow centralizadas no backend.
- Utilizar uma arquitetura frontend moderna e sustentável.
- Demonstrar um fluxo corporativo completo de ponta a ponta.

---

## Arquitetura

A solução utiliza uma arquitetura em camadas.

```text
┌───────────────────────────────┐
│           React UI            │
│                               │
│ React + TypeScript            │
│ React Router                  │
│ TanStack Query                │
│ React Hook Form + Zod         │
└───────────────┬───────────────┘
                │
                │ HTTP / JSON
                ▼
┌───────────────────────────────┐
│       ASP.NET Core API        │
│                               │
│ Controllers                   │
│ Endpoints REST                │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│      Application Layer        │
│                               │
│ Commands / Casos de Uso       │
│ Application Handlers          │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│          Domain Layer         │
│                               │
│ Entidades                     │
│ Regras de Workflow            │
│ Regras de Negócio             │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│      Infrastructure Layer     │
│                               │
│ Entity Framework Core         │
│ Repositórios                  │
│ SQL Server                    │
└───────────────────────────────┘
```

O frontend não contém as regras de negócio do workflow.

A aplicação React decide quais ações devem ser exibidas de acordo com o estado atual, enquanto o backend continua sendo responsável por validar se uma transição de workflow é permitida.

---

## Workflow da Solicitação

O principal objeto de negócio da versão 1 é uma `Request`.

O fluxo é:

```text
                  ┌───────────┐
                  │   Draft   │
                  └─────┬─────┘
                        │
                        │ Enviar
                        ▼
                ┌───────────────┐
                │  UnderReview  │
                └───────┬───────┘
                        │
               ┌────────┴────────┐
               │                 │
               │ Aprovar         │ Reprovar
               ▼                 ▼
        ┌────────────┐     ┌────────────┐
        │  Approved  │     │  Rejected  │
        └────────────┘     └────────────┘
```

Uma solicitação pode ser editada somente enquanto estiver em `Draft`.

Após o envio, ela passa para `UnderReview` e poderá ser aprovada ou reprovada.

---

## Funcionalidades da Versão 1

A versão `1.0.0` implementa o fluxo básico completo de solicitações.

### Solicitações

- Listagem de solicitações.
- Visualização de detalhes.
- Criação de nova solicitação.
- Edição enquanto estiver em Draft.
- Envio para análise.
- Aprovação de solicitações em análise.
- Reprovação de solicitações em análise.

### Frontend

- Roteamento no lado cliente.
- Gerenciamento de estado de servidor com TanStack Query.
- Formulários com React Hook Form.
- Validação com Zod.
- Estados de carregamento.
- Tratamento de erros de API.
- Invalidação de cache após mutations.
- Diálogos de confirmação com SweetAlert2.
- Formatação monetária em Real brasileiro.
- Formatação de datas em `pt-BR`.

### Backend

- REST API com ASP.NET Core.
- Arquitetura em camadas.
- Application handlers.
- Validação de workflow no domínio.
- Abstração de repositório.
- Entity Framework Core.
- Persistência em SQL Server.
- Configuração de CORS para o frontend React.
- Testes automatizados de domínio e aplicação.

---

## Status Técnico x Status Exibido

Os valores técnicos do workflow são mantidos separados dos textos apresentados ao usuário.

Exemplo:

```json
{
  "status": "UnderReview",
  "statusDescription": "Em análise"
}
```

O status técnico é usado pela lógica da aplicação:

```ts
request.status === 'UnderReview'
```

Enquanto a descrição localizada é usada pela interface:

```tsx
request.statusDescription
```

Essa separação prepara a aplicação para uma futura internacionalização sem alterar as regras do workflow.

Mapeamentos atuais:

| Status Técnico | PT-BR |
|---|---|
| `Draft` | Rascunho |
| `UnderReview` | Em análise |
| `Approved` | Aprovado |
| `Rejected` | Reprovado |

---

## Tecnologias Utilizadas

### Backend

- C#
- .NET / ASP.NET Core
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- xUnit

### Frontend

- React
- TypeScript
- Vite
- React Router
- TanStack Query
- React Hook Form
- Zod
- SweetAlert2
- CSS

### Desenvolvimento

- Visual Studio
- Visual Studio Code
- Git
- GitHub
- npm

---

## Estrutura do Repositório

```text
ModernizationFlow
│
├── backend
│   ├── ModernizationFlow.Api
│   ├── ModernizationFlow.Application
│   ├── ModernizationFlow.Domain
│   ├── ModernizationFlow.Infrastructure
│   └── ModernizationFlow.Tests
│
├── frontend
│   └── modernization-flow-web
│
├── docs
│
├── .github
│
├── README.md
└── LEIAME.md
```

---

## Projetos do Backend

### `ModernizationFlow.Domain`

Contém o modelo de domínio e as principais regras de negócio.

Responsabilidades:

- Entidade Request.
- Estados de workflow.
- Validação de transições.
- Comportamentos de domínio.

### `ModernizationFlow.Application`

Contém os casos de uso da aplicação.

Responsabilidades:

- Commands.
- Handlers.
- DTOs.
- Orquestração da aplicação.

### `ModernizationFlow.Infrastructure`

Contém as preocupações de infraestrutura.

Responsabilidades:

- Entity Framework Core.
- Integração com SQL Server.
- Implementações dos repositórios.
- Configuração da persistência.

### `ModernizationFlow.Api`

Expõe as funcionalidades da aplicação através de endpoints REST.

Responsabilidades:

- Controllers.
- Contratos HTTP.
- Dependency Injection.
- CORS.
- Swagger.

### `ModernizationFlow.Tests`

Contém os testes automatizados das regras de domínio e aplicação.

---

## Estrutura do Frontend

A aplicação React utiliza uma organização orientada a funcionalidades.

```text
src
│
├── api
│   └── httpClient.ts
│
├── components
│   ├── common
│   │   └── dialogs
│   │       └── confirmDialog.ts
│   │
│   └── layout
│
├── features
│   └── requests
│       ├── api
│       ├── components
│       ├── pages
│       ├── schemas
│       └── types
│
├── pages
│
├── routes
│
├── App.tsx
└── main.tsx
```

Os identificadores do código são escritos em inglês, enquanto o conteúdo apresentado ao usuário na versão 1 está em português.

---

## API REST

A API de solicitações disponibiliza os seguintes endpoints:

| Método | Endpoint | Descrição |
|---|---|---|
| `GET` | `/api/requests` | Lista solicitações |
| `GET` | `/api/requests/{id}` | Obtém os detalhes de uma solicitação |
| `POST` | `/api/requests` | Cria uma solicitação |
| `PUT` | `/api/requests/{id}` | Atualiza uma solicitação |
| `POST` | `/api/requests/{id}/submit` | Envia uma solicitação para análise |
| `POST` | `/api/requests/{id}/approve` | Aprova uma solicitação |
| `POST` | `/api/requests/{id}/reject` | Reprova uma solicitação |

---

## Executando o Projeto

### Pré-requisitos

Instale:

- .NET SDK
- SQL Server
- Node.js
- npm
- Git

---

## Backend

A partir da raiz do repositório:

```bash
cd backend/ModernizationFlow.Api
```

Restaure as dependências:

```bash
dotnet restore
```

Execute a API:

```bash
dotnet run
```

Durante o desenvolvimento, a API HTTPS está configurada em:

```text
https://localhost:7184
```

O Swagger está disponível em:

```text
https://localhost:7184/swagger
```

---

## Banco de Dados

O backend utiliza SQL Server através do Entity Framework Core.

Crie ou atualize o banco utilizando as migrations do EF Core.

Exemplo utilizando o Package Manager Console do Visual Studio:

```powershell
Update-Database -StartupProject ModernizationFlow.Api
```

Ou utilize a CLI do .NET conforme a configuração local do Entity Framework Core.

As configurações de conexão com o banco devem ser informadas nos arquivos de configuração do ASP.NET Core para o ambiente de desenvolvimento.

---

## Frontend

Acesse:

```bash
cd frontend/modernization-flow-web
```

Instale as dependências:

```bash
npm install
```

Crie o arquivo:

```text
.env.development
```

com:

```env
VITE_API_BASE_URL=https://localhost:7184/api
```

Inicie o servidor de desenvolvimento:

```bash
npm run dev
```

A aplicação normalmente ficará disponível em:

```text
http://localhost:5173
```

---

## Build de Produção

Para validar e gerar o build do React:

```bash
npm run build
```

Para executar a análise estática:

```bash
npm run lint
```

A versão 1 foi validada com sucesso utilizando os dois comandos.

---

## Testes Automatizados

Os testes do backend podem ser executados pelo Test Explorer do Visual Studio ou pela CLI do .NET.

Exemplo:

```bash
dotnet test
```

A versão 1 possui testes automatizados cobrindo cenários importantes de workflow e aplicação, incluindo:

- Criação de solicitação.
- Envio a partir de Draft.
- Fluxo de aprovação.
- Tentativas de aprovação inválidas.
- Fluxo de reprovação.

No fechamento da versão 1:

```text
Testes: 5
Aprovados: 5
Falhas: 0
```

---

## Fluxo Ponta a Ponta

Um fluxo típico de sucesso percorre:

```text
Formulário React
    ↓
React Hook Form
    ↓
Validação Zod
    ↓
TanStack Mutation
    ↓
HTTP Client
    ↓
ASP.NET Core Controller
    ↓
Application Handler
    ↓
Domain
    ↓
Repository
    ↓
Entity Framework Core
    ↓
SQL Server
```

Após as mutations, o TanStack Query invalida os caches correspondentes e obtém novamente o estado atual da API.

Exemplo:

```text
Editar Solicitação
    ↓
PUT /api/requests/{id}
    ↓
Banco atualizado
    ↓
Invalidate ['requests']
Invalidate ['requests', id]
    ↓
Interface atualizada
```

---

## Estratégia de Modernização

Uma das principais ideias demonstradas por este repositório é que modernizar uma aplicação não exige necessariamente uma reescrita completa.

Uma aplicação legada pode evoluir de forma incremental:

```text
Etapa 1
MVC Legado
Regras fortemente acopladas à aplicação

        ↓

Etapa 2
Extração das regras e casos de uso

        ↓

Etapa 3
Exposição dos casos de uso através de APIs REST

        ↓

Etapa 4
Introdução do React em funcionalidades selecionadas

        ↓

Etapa 5
Migração gradual de novas telas
```

Essa abordagem permite a convivência entre funcionalidades legadas e modernas durante o período de migração.

Entre os benefícios estão:

- Menor risco de migração.
- Entregas menores e incrementais.
- Maior facilidade de rollback.
- Adoção gradual de novas tecnologias pela equipe.
- Continuidade do negócio.
- Priorização das telas com maior valor.
- Reutilização do conhecimento e das regras existentes no backend.

---

## Decisões de Arquitetura

Algumas decisões foram intencionais na versão 1.

### TanStack Query em vez de Redux global

Os dados vindos do servidor são tratados como server state e gerenciados pelo TanStack Query.

Para o escopo atual, Redux não é necessário.

### React Hook Form + Zod

Os formulários permanecem tipados e validados, mantendo o código da interface relativamente simples.

### As regras de workflow permanecem no backend

O frontend controla a experiência do usuário, mas o backend continua sendo a fonte de verdade para validar transições de estado.

### Status técnico separado do status exibido

Os valores técnicos permanecem estáveis independentemente do idioma da interface.

### Estrutura frontend orientada a funcionalidades

Páginas, hooks, schemas e tipos relacionados a solicitações permanecem agrupados dentro da mesma feature.

### Diálogo de confirmação reutilizável

O SweetAlert2 está encapsulado através do helper `confirmDialog`, evitando o acoplamento direto da biblioteca às páginas.

---

## Escopo da Versão 1

O objetivo da versão 1 é intencionalmente limitado.

Incluído:

- Fluxo CRUD de solicitações.
- Transições de workflow.
- Integração React/API.
- Persistência em SQL Server.
- Validação de formulários.
- Gerenciamento de cache.
- Testes automatizados de backend.
- Descrição localizada de status.
- Interface de confirmação reutilizável.

Não incluído na versão 1:

- Autenticação.
- Autorização e perfis de acesso.
- Motivo de reprovação.
- Histórico de workflow.
- Internacionalização dinâmica.
- Dashboard.
- Notificações.
- Pipeline de produção.
- Demonstração completa de coexistência com MVC legado.

Essas funcionalidades podem ser adicionadas progressivamente em versões futuras.

---

## Próximos Passos Possíveis

Versões futuras poderão explorar:

- Autenticação e autorização.
- Perfis de solicitante e aprovador.
- Motivo de reprovação.
- Histórico do workflow.
- Audit logging.
- Internacionalização completa para PT-BR, EN-US e ES.
- Paginação, filtros e ordenação.
- Dashboard e métricas de workflow.
- Notificações.
- Testes automatizados do frontend.
- Testes de integração.
- CI/CD com GitHub Actions ou Azure DevOps.
- Containerização com Docker.
- Observabilidade e logs estruturados.
- Migração de novos módulos MVC.
- Execução lado a lado entre MVC legado e React.

---

## Versão

Versão estável atual:

```text
v1.0.0
```

A versão 1 estabelece a fundação técnica para uma migração incremental de uma aplicação baseada em ASP.NET para um frontend moderno em React, preservando uma arquitetura estruturada em .NET.

---

## Autor

**Andaramis Bezerra**

Desenvolvedor de Software Sênior / Arquiteto de Software

Principais áreas de experiência:

- C# / .NET
- ASP.NET Core
- ASP.NET MVC
- REST APIs
- SQL Server
- Arquitetura de aplicações corporativas
- React
- Modernização de aplicações