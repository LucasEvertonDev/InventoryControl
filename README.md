# InventoryControl

## Instalação

Para instalar as dependencias clone o projeto e execute o seguinte comando dentro da pasta.

```shell
dotnet build .\InventoryControl.WebUI.csproj
```

logo após execute o sistema:

```
dotnet run InventoryControl.WebUI.dll
```

### Pré-requisitos

Para executar este container deverá ter o Docker instalado.

* [Windows](https://docs.docker.com/windows/started)
* [OS X](https://docs.docker.com/mac/started/)
* [Linux](https://docs.docker.com/linux/started/)

### Containers

#### Compose Project

List the different parameters available to your container

O projeto já possui um docker-compose configurado, conseguirá executar o projeto somente executando o código:

```shell
docker-compose up
```

Para construir a Imagem do Docker:

```shell
docker build -t inventorycontrol .
```

Para executar o Docker na porta 80:

```shell
docker container run --name inventorycontrol -p 80:80 inventorycontrol
```

## Authors

* **Lucas Everton** - *Initial work* - [LucasEvertonDev](https://github.com/LucasEvertonDev)
* **Pedro Rodrigues** - *Complements* - [pedrovitorrs](https://github.com/pedrovitorrs)

## Como contribuir

Esteja sempre atento à criação de novas branches, padronização de commits e comentários em código
para que possamos melhorar sua mantenabilidade.