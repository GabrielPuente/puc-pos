# AWS trabalho final
Gabriel Augusto Nicoletti Puente - 141607


.Net 6 +  ORM EF - Code first

Padrão mediator com pacote do MediatR

Notification pattern juntamente com os commands e dominios

Flunt para validação das entidades

Swagger para documentacao viva da API

Lib SqlKata + Dapper para fazer as queries e trazendo numa viewmodel

Autenticacao e autorizacao utilziando JWT Bearer Token e identity Claims

RabbitMQ para eventos de acontecimentos em partidas. Padrao (Pub/Sub)


Role no cadastro de usuario se remete a autorizacao na api, tendo dois tipo "Player" e "Coach". 
Onde "Player" pode somente fazer os Get da api, quando todos os Post e Put só podem ser realizados pelo usuario cujo role é "Coach"
