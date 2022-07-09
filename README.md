# Parmezzan

Wymagania:
  - sql server
  - rabbitMQ (docker run -d --hostname my-rabbit --name some-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management  )
  
Przed uruchomieniem projektu należy zmienić connectionstring (appsettings.json) oraz wykonać migracje dla poszczególnych microservice'ów:
  - Parmezzan.Service.ProductAPI
  - Parmezzan.Service.OrderAPI
  - Parmezzan.Service.ShoppingCartAPI
  - Parmezzan.Service.Identity
  - Parmezzan.Service.EmailSender
