# Blog Simples com ASP.NET

Este projeto é um sistema básico de blog onde os usuários podem visualizar, criar, editar e excluir postagens. O projeto tem como finalidade o uso de  WebSockets para notificar os usuários sobre novas postagens em tempo real.
O projeto foi desenvolvido com os seguintes objetivos e tecnologias:

## Funcionalidades

- **Visualizar Postagens:** Os usuários podem ver todas as postagens disponíveis.
- **Criar Postagens:** Os usuários autenticados podem criar novas postagens.
- **Editar Postagens:** Os usuários autenticados podem editar suas postagens existentes.
- **Excluir Postagens:** Os usuários autenticados podem excluir suas postagens.

## Tecnologias Utilizadas

- **ASP.NET:** Framework utilizado para o desenvolvimento do backend.
- **Entity Framework:** Utilizado para a manipulação e persistência de dados.
- **WebSockets:** Implementado para notificar os usuários em tempo real sobre novas postagens.
- **Banco de dados:** MySql. 

## Princípios de Desenvolvimento

Este projeto foi desenvolvido seguindo os princípios de orientação a objetos e os princípios SOLID para garantir um código mais limpo, modular e fácil de manter.

## Instalação

1. Clone o repositório:
   ```bash
   git clone https://github.com/SEU_USUARIO/SEU_REPOSITORIO.git
   cd SEU_REPOSITORIO

## Como Usar

- **Abra o projeto no Visual Studio**
1. Clique com botão direito do mouse na Solution.
2. Clique em Configurar projeto de inicialização, marque a opção **vários projetos de inicialização** e depois na coluna escolha **Iniciar** e depois confirme no botão de **OK**
   
   ![image](https://github.com/user-attachments/assets/e72feae1-622d-4cea-9603-b140e29d563d)
   
3.Agora clique para executar o projeto no botão de **Iniciar sem depurar**, isso fara que o **Servidor WebSocket inicia junto com API **

![image](https://github.com/user-attachments/assets/7a6f9a5b-6996-4fc0-90fd-5eaf69b08e5d)

4. Depois disso, coloque as duas lado a lado para visualizar o resultado.
   
![image](https://github.com/user-attachments/assets/fab1bee7-7e4d-4254-b51b-46b4d9867286)

6. Crie um cadastro e faça login, logo apos realize o login e copie o token gerado para validar.
  
![image](https://github.com/user-attachments/assets/b7086f33-a6ac-49ec-831f-feea4c9b9503)

Validar

![image](https://github.com/user-attachments/assets/144a4c7e-a959-43ad-a686-02ffb881097a)

## Resultado

Inserção de um novo post que notifica todos os usuários com WebSocket.

![image](https://github.com/user-attachments/assets/8510766b-4821-46b5-a1e5-edaed903de3f)

Este projeto está licenciado sob a licença MIT.



 



