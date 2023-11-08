# CurrentAccountApi

Entre na pasta src dentro da pasta raiz através do comando:

``
cd src
``

Crie a imagem docker e rode através dos comandos:

``
docker build -f Dockerfile -t currentaccountapi .
``

``
docker run --name currentaccountapi -d -p 7034:80 currentaccountapi
``
