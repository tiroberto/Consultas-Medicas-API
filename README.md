# 🏥 Sistema de Consultas Médicas

Este projeto é uma API RESTful para gerenciamento de consultas médicas, desenvolvido como parte do meu portfólio. O sistema permite cadastrar e gerenciar **médicos, pacientes, especialidades, consultas** com **autenticação JWT**.

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

- **.NET 8** com **C#**
- **Entity Framework Core**
- **SQLite** como banco de dados local (possível trocar por outro banco de dados SQL)

### 🛠️ Padrões e Técnicas

- Programação orientada a objetos (POO)
- Padrão **Repository** e **DTO (Data Transfer Object)**
- Injeção de dependência
- Separacão de camadas:
  - `Aplicacao` (Serviços e regras de negócio)
  - `Comum` (Funções e utilitários)
  - `Dominio` (Entidades)
  - `Repositorio` (Acesso a dados)
  - `WebAPI` (Camada de apresentação)

### 📦 Outras Tecnologias

- **Swagger** para documentação da API

---

## 📁 Estrutura do Projeto

```
ConsultasMedicas/
├── Aplicacao/
├── Comum/
├── Dominio/
├── Repositorio/
├── WebAPI/
└── ConsultasMedicas.sln
```

---

## 🧪 Como Rodar Localmente

1. Clone o repositório:

   ```bash
   git clone https://github.com/tiroberto/Consultas-Medicas-API.git
   ```

2. Acesse a pasta:

   ```bash
   cd Consultas-Medicas-API
   ```

3. Restaure os pacotes:

   ```bash
   dotnet restore
   ```

4. Execute a aplicação:

   ```bash
   dotnet run --project WebAPI
   ```

5. Login para testes:

   ```json
   {
     "usuarioId": 0,
     "nome": "",
     "email": "admin@admin.com",
     "senha": "admin123",
     "tipoUsuarioId": 0
   }
   ```

---

## 📄 Exemplos de Requisições JSON

### 🔹 Marcar uma consulta: /Consulta/adicionar
  - Deve-se adicionar o token de autenticação no header da requisição

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

