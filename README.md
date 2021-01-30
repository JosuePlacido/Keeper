### Estrutura

-   <s>Logo ou Banner</s>
-   <s>Demonstração da Aplicação</s>
-   <s>Título e Descrição</s>
-   Badges
-   Status do Projeto
-   Tabela de Conteúdos
-   Features
-   Pré-requisitos e como rodar a aplicação/testes
-   Tecnologias utilizadas
-   Contribuição
-   Autor
-   Licença

# Torneios FC

![Banner](./.github/banner.png)

<p align="center">
  <img alt="GitHub language count" src="https://img.shields.io/github/languages/count/josueplacido/keeper?color=%2304D361">

  <img alt="Repository size" src="https://img.shields.io/github/repo-size/josueplacido/keeper">

  <a href="https://github.com/josueplacido/keeper/commits/master">
    <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/josueplacido/keeper">
  </a>

  <img alt="License" src="https://img.shields.io/github/license/josueplacido/keeper">
   <a href="https://github.com/tgmarinho/nlw1/stargazers">
    <img alt="Stargazers" src="https://img.shields.io/github/stars/josueplacido/keeper?style=social">
  </a>

  <img alt="Github issues" src="https://img.shields.io/github/issues/JosuePlacido/keeper?color=56BEB8" />

  <img alt="Github forks" src="https://img.shields.io/github/forks/JosuePlacido/keeper?color=56BEB8" />

<img alt="Status" src="https://img.shields.io/static/v1?label=status&message=Em%20Desenvolvimento&color=orange&style=flat"/>

<img alt="Status" src="https://juzao.visualstudio.com/61c713ff-3c97-4bf5-a734-54883d06ab60/bc79f462-8764-44f5-86f8-5214a7183931/_apis/work/boardbadge/053e7275-ab1d-4f0b-83d9-ecfec19d98f7?columnOptions=1">

</p>

## Sobre o projeto

⚽ Torneios FC, permite criar e gerenciar campeonatos de diferentes regulamentos
de maneira simples e inuitiva, além de dispor de um aplicativo móvel para facilitar o compartilhamento das estatísticas do campeonato, como classificação e lista de artilheiros.

Que tal mostrar para seus amigos que vc eh o homem gol da sua cidade? 😎

## Screenshots

🚧 em construção 🚧

## :link: Links e demos

Prótótipo BETA que está sendo refatorado
[torneiosfc](josueplacido1-001-site2.btempurl.com).

🚧 em construção 🚧

## Tabela de conteúdos

=================

-   [Sobre](#sobre-o-projeto)
-   [Screenshot](#screenshots)
-   [Links e demos](#link-links-e-demos)
-   [Tabela de Conteudo](#tabela-de-conteúdos)
-   [Funcionalidades](#funcionalidades)

    -   [Site](#site)
        -   [Backend](#backend)
        -   [Frontend](#frontend)
    -   [Aplicativo](#site)

-   [Instalação](#instalação)
    -   [Pre Requisitos](#pré-requisitos)
    -   [Backend](#rodando-o-backend)
    -   [Frontend](#rodando-o-frontend)
    -   [Mobile](#rodando-o-mobile)
    -   [Tests](#rodando-os-testes)
-   [Tecnologias](#tecnologias)
-   [Conceitos e padrões](#conceitos-e-padrões)
-   [Autor](#autor)
-   [Licença](#licença)

## Funcionalidades

Estão listadas abaixo algumas funcionalidades previstas e o andamento delas para esta primeira versão:

### Site

#### Backend

-   [x] Criar campeonato
-   [x] Alterar plantel dos times inscritos
-   [x] Gerenciar Jogadores
-   [x] Gerenciar Times
-   [x] Alterar estatísticas e rank
-   [ ] Editar Agenda de jogos
-   [ ] Renomear itens gerados Grupos, Fases e jogos
-   [ ] Registrar súmula
-   [ ] Atualiar campeonato automaticamente conforme for necessário após o registro de súmula

### Frontend

-   [ ] Criar campeonato
-   [ ] Alterar plantel dos times inscritos
-   [ ] Gerenciar Jogadores
-   [ ] Gerenciar Times
-   [ ] Alterar estatísticas e rank
-   [ ] Editar Agenda de jogos
-   [ ] Renomear itens gerados Grupos, Fases e jogos
-   [ ] Registrar súmula
-   [ ] Atualiar campeonato automaticamente conforme for necessário após o registro de súmula

### Aplicativo

-   [ ] Exibir os jogos do dia
-   [ ] Exibir a agenda de jogos de cada campeonato
-   [ ] Exibir os artilheiros de cada campeonato
-   [ ] Exibir a tabela de classificação e chaveamento de cada campeonato
-   [ ] Exibir os jogadores com mais prêmios MVP
-   [ ] Exibir os jogadores com mais cartões amarelos e vermelhos MVP

## Instalação

Este projeto é divido em três partes:

1. Backend (pasta api)
2. Frontend (pasta web)
3. Mobile (pasta mobile)

💡Tanto o Frontend quanto o Mobile precisam que o Backend esteja sendo executado para funcionar.

### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:

-   [Git](https://git-scm.com);
-   [Node.js](https://nodejs.org/en/);
-   [SDK .NET Core 5.0](https://dotnet.microsoft.com/download);
-   [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads);
-   Editor ou IDE de sua preferência. Recomendo o [VSCode](https://code.visualstudio.com/) ou [Visual Studio](https://visualstudio.microsoft.com/)

#### Rodando o Backend

Ajuste a connectionStrings no arquivo "appsettings.development.json" que está na pasta "projects/api" para o SQLServer local

```bash

# Clone este repositório
$ git clone git@github.com:josueplacido/keeper.git

# Instale as dependências globalmente do Monorepo
$ npm install

# Acesse a pasta do projeto no terminal/cmd
$ cd projects/api

# Execute a aplicação em modo de desenvolvimento
$ dotnet run

# O servidor inciará na porta:5000 ou 5001 - acesse http://localhost:5000

```

#### Rodando o Frontend

🚧 Em construção 🚧

#### Rodando o Mobile

🚧 Em construção 🚧

#### Rodando os testes

Para rodar os testes de unidade, integração e EndToEnd do BANCKEND:

```bash

# Acesse a pasta do projeto de teste no terminal/cmd
$ cd projects/test

# Execute a aplicação em modo de desenvolvimento
$ dotnet test

```

## Tecnologias

-   Net Core 5.0
-   Swagger
-   SQL Server
-   VS CODE
-   NPM
-   YARN
-   JSON
-   REACT
-   REACT NATIVE
-   STYLED-COMPONENTS
-   TYPESCRIPT
-   HTML
-   CSS/SASS
-   GIT
-   AZURE BOARDS
-   ENTITY-FRAMEWORK

## Conceitos e padrões

Lista de conceitos, padrões, técnicas e metodologias estudada e/ou aplicada no projeto.

-   POO
-   DDD
-   MONOREPO
-   FACTORY
-   KANBAN
-   CLEAN ARCHITETURE

---

## Autor

<a alt="Linkedin" href="https://linkedin/in/josueplacido">
 <img style="border-radius: 50%;" src="https://github.com/josueplacido.png" width="100px;" alt=""/>
 <br />
 <sub><b>Josué Placido</b></sub></a>

Feito com ❤️ por Josué Placido 👋🏽 Entre em contato!

[![Linkedin Badge](https://img.shields.io/badge/-Josue%20Placido-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/josueplacido/)](https://www.linkedin.com/in/josueplacido/)
[![Gmail Badge](https://img.shields.io/badge/-juplacido.jnr@gmail.com-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:juplacido.jnr@gmail.com)](mailto:juplacido.jnr@gmail.com)
[![Hotmail Badge](https://img.shields.io/badge/-ozzyplacidojunior@hotmail.com-blue?style=flat-square&logo=microsoft&link=mailto:ozzyplacidojunior@hotmail.com)](mailto:ozzyplacidojunior@hotmail.com)

## 📝 Licença

Este projeto esta sobe a licença [MIT](./LICENSE).
