### REST API

+ GET /items
    - выводит все позиции
+ GET /item/{id}
    - найти позицию по id
+ POST /items
    - добавить позицию
+ PUT /items/{id}
    - изменить имя/цену позиции.
+ DELETE /items/{id}
    - удалить позицию по id

#### dotnet
* dotnet add package mongodb.driver
* dotnet add package AspNetCore.HealthChecks.MongoDb

#### MongoDB
* docker run -d --rm --name <\image-name> -p 27017:27017 -v <\mongodbdata:/data/db(local)> <\image-name>

#### Docker
* docker run -d --rm --name <\image-name> -p 27017:27017 -v <\mongodbdata:/data/db(local)> -e MONGO_INITDB_ROOT_USERNAME=<\username> -e MONGO_INITDB_ROOT_PASSWORD=<\password> <\image-name>
* dotnet user-secrets init
* dotnet user-secrets set MongoDb:Password <\password>

#### Docker-nework
* docker build -t collection:v1 .
* docker network create <\network-name>
* docker run -d --rm --name <\image-name> -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=<\username> -e MONGO_INITDB_ROOT_PASSWORD=<\password> --network=<\network-name> <\image-name>
* docker run -it --rm -p 8080:80 -e MongoDb:Host=<\image-name> MongoDb:Password=password --network=<\network-name> collection:v1

#### Kubernetes
* kubectl config current-context
* kubectl create secret generic collection-secrets --from-literal=mongodb-password='password'
* kubectl apply -f .\collection.yaml
* kubectl get deployments
* kubectl get pods
* kubectl logs nameofthepod
* kubectl apply -f .\mongodb.yaml