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
Configure as variáveis de ambiente conforme necessário para o MongoDB e RabbitMQ.

2. **Rode a aplicação utilizando o comando:

    ```bash
      dotnet run --project src/FluxoCaixa.Application.WebApi

3. **Considerações Finais
Melhorias Futuras: Para tornar o projeto mais robusto, considere as seguintes implementações:

Adoção de um cache distribuído como Redis para melhorar a performance das consultas de dados frequentes.
Implementação de CQRS (Command Query Responsibility Segregation) para separar as operações de leitura e escrita, aumentando a escalabilidade e facilitando a manutenção.
Monitoramento e observabilidade: Integrar ferramentas como Prometheus e Grafana para monitoramento de métricas e alertas em tempo real.
Outras Sugestões:

Utilizar Docker para facilitar o deploy e o ambiente de desenvolvimento, encapsulando a aplicação e suas dependências em containers.
Implementar Testes de Integração utilizando ferramentas como xUnit e Moq para garantir a qualidade do código e reduzir a ocorrência de bugs.
