## Account API

## Tecnologias

- .NET Core 2.2
- C#
- xUnit
- MassTransit (https://masstransit-project.com/)

## Instruções para o build

Navegue até a pasta da solução, onde esta o arquivo Challenge.Api.sln

depois execute o comando:

dotnet build Challenge.Api.sln

e para rodar a solução excutar o seguinte comando:

docker-compose up

Lembro que todos os endpoints devem receber no header Api-Key o valor 8999356b-62e5-4f73-b775-107dbe925c99

## Notas

- Para essa solução priorizei uma arquitetura orientada e mensageria, no caso utilizei o MassTransit para poder simular filas em memoria e ao mesmo tempo, posso configurar para utilizar sistemas como RabbitMQ, Azure Service Bus dentre outros. Facilitando assim desacoplagem de serviços especificos.
- Também adotei o sistema de filas para conseguir tratar a concorrencia, já que em ambiente de produção temos mais de uma isntancia do mesmo serviço e o proprio balanceador de carga pode redirecionar mensagens para servidores diferentes. Dessa forma, ao trabalhar com filas (queues) podemos definir politicas de ordenamento de mensagens ou mesmo de quantidade que o consumidor ira trabalhar. 
- Para segurança adotei um middleware do .NET Core que permite validar todas as request e bater o header Api-Key com uma chave configurada no appsettings.json
- Como esse foi apenas um endpoint de um microserviço, não foi possível mostrar uma solução completa utilizando microserviço, mas na prática padrões como conexões resilientes e SAGA facilitam e garantem que o fluxo seja atingido da melhor forma, apenas tomando bastante cuidado com largura de banda