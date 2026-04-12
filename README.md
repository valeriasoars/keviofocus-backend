# KevioFocus Backend

Este é o backend do projeto **KevioFocus**, desenvolvido em **.NET 10** utilizando **Entity Framework Core** e **SQL Server**.

## Tecnologias Utilizadas

- .NET 10 (ASP.NET Core Web API)
- Entity Framework Core (ORM)
- SQL Server (Banco de Dados)
- User Secrets (Gerenciamento de Segredos)

## Configuração de Segurança (Importante)

As credenciais do banco **não estão** no arquivo `appsettings.json`.  
Elas devem ser configuradas localmente usando o **Secret Manager do .NET**.

### 1. Configurar User Secrets

Abra o terminal na raiz do projeto e execute:

```
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=SEU_SERVIDOR;Database=KevioFocusDb;Trusted_Connection=True;TrustServerCertificate=True"
```

### 2. Verificar Segredos

```
dotnet user-secrets list
```

## Instalação e Dependências

Para restaurar os pacotes do projeto:

```
dotnet restore
```

### Principais pacotes utilizados:
- Microsoft.EntityFrameworkCore.SqlServer (10.0.5)
- Microsoft.EntityFrameworkCore.Design (10.0.5)
- Microsoft.EntityFrameworkCore.Tools (10.0.5)

## Banco de Dados (Migrations)

O projeto utiliza Migrations para versionamento do banco.

1. Instalar ferramenta EF Core (caso necessário)
```
dotnet tool install --global dotnet-ef
```
3. Aplicar as migrations
```
dotnet ef database update --context KevioDbContext
```

##  Testando a API com Scalar

O projeto utiliza o **Scalar** (alternativa moderna ao Swagger) para documentação e testes manuais da API.

### Como acessar:
1. Certifique-se de que o projeto está rodando (`dotnet run`).
2. Acesse no seu navegador: `https://localhost:PORTA/scalar/v1`
   *(Substitua PORTA pela porta exibida no seu console, ex: 7000).*
