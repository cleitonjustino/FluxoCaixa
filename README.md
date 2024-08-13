# Projeto Fluxo de Caixa

## Tecnologias Utilizadas

O projeto foi construído utilizando as seguintes tecnologias:

- **.NET Core 8**: Utilizado como framework principal para o desenvolvimento da aplicação devido à sua robustez e suporte a desenvolvimento cross-platform.
- **MongoDB**: Escolhido como banco de dados pela sua alta performance e flexibilidade em lidar com grandes volumes de dados não estruturados.
- **RabbitMQ**: Implementado para o gerenciamento e processamento assíncrono do consolidado diário, garantindo a eficiência e escalabilidade do sistema.

# Arquitetura e Padrões Utilizados

O projeto segue uma arquitetura baseada nos princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**. Além disso, a aplicação utiliza o padrão **CQRS (Command Query Responsibility Segregation)** para separar as operações de leitura e escrita, melhorando a escalabilidade e a organização do código.

A construção dos objetos no sistema é facilitada pelo uso do **Pattern Builder**, garantindo que a criação de entidades complexas seja feita de forma controlada e segura.

## Desenho da Solução / Arquitetura

O projeto foi desenvolvido seguindo os princípios de Clean Architecture e Domain-Driven Design (DDD). A imagem abaixo ilustra a arquitetura utilizada:

![Desenho da Solução](https://github.com/cleitonjustino/FluxoCaixa/blob/main/documentos/Fluxo.png)

![Desenho da Solução](https://github.com/cleitonjustino/FluxoCaixa/blob/main/documentos/FlxTechs.png)

## Executando o Projeto

Para executar a aplicação localmente, siga os passos abaixo:

1. **Clone o repositório**:

   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio

2. **Configure as variáveis de ambiente conforme necessário para o MongoDB e RabbitMQ**.

3. **Rode a aplicação utilizando o comando**:
   ```bash
      docker-compose up -d
      dotnet run --project src/FluxoCaixa.Application.WebApi

4. **Endpoints**:
   

4. **Considerações Finais**
Melhorias Futuras: Para tornar o projeto mais robusto, considere as seguintes implementações:
- Autenticação JWT Token
- Retorno de status code adequadamente
- Uso de FluentValidator para validações de entrada
- Adoção de um cache distribuído como Redis para melhorar a performance das consultas e geração dos relatórios com os dados frequentes.
- Controle de mensageria em geração de relatórios, além de uma possível utilização de um novo microserviço para esta geração.
- Monitoramento e observabilidade: Integrar ferramentas como Prometheus e Grafana para monitoramento de métricas e alertas em tempo real.
- Implementar Testes de Integração utilizando ferramentas como JMeter ou Postman para garantir a qualidade do código, número de requisições simultâneas e reduzir a ocorrência de bugs.
