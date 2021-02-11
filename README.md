# Weather Web API em .NET Core 3.1

O projeto tem como intuito ser um laboratório para testar ferramentas, implementações e ideias.

Atualmente, ela se integra com a API do OpenWeatherMap para buscar dados climáticos através do nome da cidade.

Algumas implementações e ferramentas que estão no projeto:
- Solução organizada com base nos princípios do DDD e CQRS
- MediatR como mediador dos Commands, Queries e Notifications
- Entity Framework Core como ORM
- Integração REST com a API do OpenWeatherMap
- Política de retry da integração REST com Polly
- Autenticação com AspNetCore Identity
- Swagger para interface de teste da Web API
- AutoMapper auxiliando nos mapeamentos dos responses das Queries
- FluentValidator auxiliando nas validações dos requests dos Commands
- Strategy aplicado em algumas situações
- Injeção de Dependencia por toda parte :)
- Testes unitários com xUnit e Moq
