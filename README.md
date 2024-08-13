# Projeto Fluxo de Caixa

## Tecnologias Utilizadas

O projeto foi construído utilizando as seguintes tecnologias:

- **.NET Core 8**: Utilizado como framework principal para o desenvolvimento da aplicação devido à sua robustez e suporte a desenvolvimento cross-platform.
- **MongoDB**: Escolhido como banco de dados pela sua alta performance e flexibilidade em lidar com grandes volumes de dados não estruturados.
- **RabbitMQ**: Implementado para o gerenciamento e processamento assíncrono do consolidado diário, garantindo a eficiência e escalabilidade do sistema.

## Desenho da Solução / Arquitetura

O projeto foi desenvolvido seguindo os princípios de Clean Architecture e Domain-Driven Design (DDD). A imagem abaixo ilustra a arquitetura utilizada:

![Desenho da Solução](link-para-imagem)

## Executando o Projeto

Para executar a aplicação localmente, siga os passos abaixo:

1. **Clone o repositório**:

   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio

2. **Execute o docker-compose:
 ```bash
docker-compose up --build

3. **Configure as variáveis de ambiente conforme necessário para o MongoDB e RabbitMQ.

