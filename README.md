# 🏥 Sistema de Consultas Médicas

Este projeto é uma API RESTful para gerenciamento de consultas médicas, desenvolvido como parte do meu portfólio. O sistema permite cadastrar e gerenciar **médicos, pacientes, especialidades, consultas** e **usuários com diferentes tipos de acesso**.

---

## 🚀 Funcionalidades

- ✅ Cadastro de pacientes e médicos
- ✅ Associação de médicos a especialidades
- ✅ Agendamento de consultas com validação de horário
- ✅ Cancelamento e atualização de consultas
- ✅ CRUD completo com regras de negócio aplicadas
- ✅ Validações automáticas com mensagens claras de erro
- ✅ Autenticação com JWT.
- ✅ API RESTful pronta para front-end ou mobile

---

## 🧰 Tecnologias e Ferramentas Utilizadas

### 🔧 Back-End

- **.NET 7 / .NET 8** com **C#**
- **Entity Framework Core**
- **SQLite** como banco de dados local

### 🛠️ Padrões e Técnicas

- Programação orientada a objetos (POO)
- Padrão **Repository** e **DTO (Data Transfer Object)**
- Injeção de dependência
- Separacão de camadas:
  - `Dominio` (Entidades)
  - `Aplicacao` (Serviços e regras de negócio)
  - `Repositorio` (Acesso a dados)
  - `WebAPI` (Camada de apresentação)

### 📦 Outras Tecnologias

- **Swagger** para documentação da API

---

## 📁 Estrutura do Projeto

```
ConsultasMedicas/
├── Aplicacao/
├── Dominio/
├── Repositorio/
├── WebAPI/
└── ConsultasMedicas.sln
```

---

## 🧪 Como Rodar Localmente

1. Clone o repositório:

   ```bash
   git clone https://github.com/seuusuario/consultas-medicas.git
   ```

2. Acesse a pasta:

   ```bash
   cd consultas-medicas
   ```

3. Restaure os pacotes:

   ```bash
   dotnet restore
   ```

4. Crie a base de dados (opcional, se não usar Migrations):

   ```bash
   dotnet ef database update
   ```

5. Execute a aplicação:

   ```bash
   dotnet run --project WebAPI
   ```

---

## 📄 Exemplos de Requisições JSON

### 🔹 Marcar uma consulta: /consulta/salvar

```json
{
  "pacienteId": 1,
  "medicoId": 2,
  "dataHoraInicio": "2025-06-10T10:30:00",
  "dataHoraFim": "2025-06-10T11:00:00",
  "statusConsulta": 1,
  "observacoes": "Retorno de exame"
}
```

---

## ✍️ Autor

Desenvolvido por **Humberto Júnior** – entre em contato comigo pelo [LinkedIn](https://linkedin.com/in/humbertoecrjunior) ou aqui no GitHub!

