# Projeto de Autenticação de Usuário utilizando JWT

Este projeto é uma implementação de um sistema de autenticação de usuário utilizando JSON Web Tokens (JWT). Ele foi desenvolvido utilizando a linguagem de programação .NET, o Entity Framework Core e o banco de dados MySQL.

## Funcionalidades

O sistema de autenticação de usuário possui as seguintes funcionalidades:

- **Signup** (registro de usuário): Os usuários podem se cadastrar no sistema fornecendo um nome de usuário e uma senha. As senhas dos usuários são processadas por um algoritmo de hash e salt antes de serem armazenadas no banco de dados. Isso garante a segurança das senhas, protegendo a informação confidencial dos usuários.

- **Signin** (login de usuário): Os usuários registrados podem fazer login fornecendo suas credenciais (nome de usuário e senha). O sistema valida as credenciais e gera um token JWT válido, que é retornado ao usuário. Esse token pode ser usado posteriormente para acessar rotas e recursos protegidos.
