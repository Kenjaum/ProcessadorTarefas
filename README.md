# 🛠️ Processador de Tarefas com Docker

Este projeto é composto por serviços que processam tarefas, utilizando **.NET**, **MongoDB**, **RabbitMQ** e **Docker**.

---

## 📦 Estrutura dos Containers

- `api`: API para gerenciar tarefas e comunicação com os workers.
- `worker_enviar_email`: Responsável por processar tarefas de envio de e-mails.
- `worker_gerar_relatorio`: Responsável por gerar relatórios.
- `rabbitmq`: Gerenciador de filas (mensageria).
- `mongodb`: Banco de dados utilizado pelos workers e pela API.

---

## 🚀 Como executar este projeto com Docker

### ✅ Pré-requisitos

- [Docker](https://www.docker.com/get-started) instalado
- [Docker Compose](https://docs.docker.com/compose/install/) (já incluído no Docker Desktop)

---

### 🔧 Passos para rodar a aplicação

1. **Clone este repositório**
   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
   ```

2. **Execute o Docker Compose**

   ```bash
   docker-compose up --build
   ```

   Isso irá:

   - Construir as imagens dos serviços  
   - Subir os containers definidos  

---

### 🌐 Acesse os serviços

  - **API**: [http://localhost:5000](http://localhost:8080/swagger/index.html) *(ajuste a porta conforme o `docker-compose.yml`)*
  - **RabbitMQ Dashboard**: [http://localhost:15672](http://localhost:15672)  
     **Usuário**: `guest`  
     **Senha**: `guest`
