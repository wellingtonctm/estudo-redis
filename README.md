# 🚀 redis-teste

Projeto de estudo que explora o uso do **Redis** como banco de dados em memória para armazenamento e recuperação de dados. Desenvolvido com **.NET 9.0** e a biblioteca **StackExchange.Redis**, este projeto implementa uma **API simples** com endpoints para interagir com o Redis.

---

## 🧱 Estrutura do Projeto

- **API .NET 9.0**
- **Redis** como banco de dados em memória
- **Docker** para ambiente local
- **Swagger** para documentação da API
- **StackExchange.Redis** para acesso ao Redis

---

## 🔌 Endpoints

### 🔍 `GET /get/{key}`

**Descrição:** Recupera o valor associado a uma chave no Redis.

**Parâmetros:**

- `key` (string): A chave a ser buscada no Redis.

**Retornos:**

- `200 OK`: Retorna o valor associado à chave.
- `404 Not Found`: Caso a chave não seja encontrada.

---

### 📝 `POST /set/{key}`

**Descrição:** Define um valor para uma chave no Redis.

**Parâmetros:**

- `key` (string): A chave a ser definida no Redis.

**Corpo da requisição:**

```json
{
  "conteudo": "valor a ser armazenado"
}
```

**Retornos:**

- `200 OK`: Valor salvo com sucesso.
- `400 Bad Request`: Conteúdo inválido ou vazio.

---

## 🧾 Modelos

### 📥 Request

Representa o corpo da requisição para o endpoint `POST /set/{key}`.

**Propriedades:**

- `Conteudo` (string): Valor a ser armazenado no Redis.

---

### ⚙️ RedisOptions

Configurações do Redis carregadas do `appsettings.json`.

**Propriedades:**

- `Enabled` (bool): Indica se o Redis está habilitado.
- `ConnectionString` (string): String de conexão.
- `Persistencia` (TimeSpan): Tempo de persistência dos dados.

---

### 🧊 RedisSetObject

Objeto armazenado no Redis.

**Propriedades:**

- `Conteudo` (string): Valor serializado.
- `Timestamp` (DateTime): Momento do armazenamento.

---

### 🧠 RedisGetObject<T>

Objeto recuperado do Redis.

**Propriedades:**

- `Conteudo` (T): Valor deserializado.
- `Timestamp` (DateTime): Momento do armazenamento.

---

### 🗂️ ERedis (Enum)

Chaves pré-definidas para o Redis.

**Valores:**

- `Default`
- `ChaveExemplo`
- `ChaveExemplo2`

---

## ⚙️ Configurações

### `appsettings.json`

Contém configurações do Redis.

```json
{
  "RedisOptions": {
    "Enabled": true,
    "ConnectionString": "localhost:6379",
    "Persistencia": "00:10:00"
  }
}
```

### `appsettings.Development.json`

Configurações específicas para ambiente de desenvolvimento.

---

## 🧰 Utilitários

### 🔧 RedisUtil

Classe responsável por interagir com o Redis.

**Métodos:**

- `GetAsync<T>(string key)`
- `SetAsync(string key, object value, TimeSpan? tempoDuracao = null)`
- `DisposeAsync()`

### 🧪 IRedisUtil

Interface que define os métodos da `RedisUtil`.

---

## 🐳 Docker

O projeto inclui um arquivo `docker-compose.yml` para rodar o Redis localmente.

### Exemplo de execução:

```bash
docker-compose up -d
```

---

## 📦 Dependências

- [`Microsoft.AspNetCore.OpenApi`](https://www.nuget.org/packages/Microsoft.AspNetCore.OpenApi) – Documentação OpenAPI  
- [`StackExchange.Redis`](https://www.nuget.org/packages/StackExchange.Redis) – Interação com Redis  
- [`Swashbuckle.AspNetCore`](https://www.nuget.org/packages/Swashbuckle.AspNetCore) – Geração do Swagger

---

## ▶️ Como Executar

1. Certifique-se de que o Redis está em execução (`docker-compose` ou instalação local).
2. Configure o `appsettings.json` com a conexão correta.
3. Execute o projeto:

```bash
dotnet run
```

4. Acesse os endpoints via Swagger: [http://localhost:5171](http://localhost:5171)

---

## 💡 Observações

Este projeto foi desenvolvido com fins **educacionais** para explorar o uso do Redis em aplicações .NET. Ele pode ser expandido com:

- Autenticação
- Cache avançado
- Integrações com outros serviços