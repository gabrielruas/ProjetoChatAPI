# Mensagens em tempo real

O GET `http://localhost:5276/Mensagem/ListarPorAtendimento/6` continua
disponível para consultar o histórico. Para receber atualizações automáticas,
conecte o frontend ao hub `http://localhost:5276/hubs/mensagens`.

Instale o cliente no projeto do frontend:

```sh
npm install @microsoft/signalr
```

```js
import { HubConnectionBuilder } from "@microsoft/signalr";

const atendimentoId = 6;
const conexao = new HubConnectionBuilder()
  .withUrl("http://localhost:5276/hubs/mensagens")
  .withAutomaticReconnect()
  .build();

conexao.on("MensagensAtualizadas", (id, mensagens) => {
  if (id === atendimentoId) {
    // Substitua pela atualização do estado da sua tela, por exemplo:
    // setMensagens(mensagens);
    console.log(mensagens);
  }
});

// Os grupos precisam ser retomados após uma reconexão.
conexao.onreconnected(async () => {
  try {
    await conexao.invoke("EntrarAtendimento", atendimentoId);
  } catch (erro) {
    console.error("Falha ao retomar atendimento", erro);
  }
});

await conexao.start();
await conexao.invoke("EntrarAtendimento", atendimentoId);

// Ao desmontar a tela: await conexao.stop();
// Para trocar de atendimento na mesma conexão:
// await conexao.invoke("SairAtendimento", atendimentoId);
// await conexao.invoke("EntrarAtendimento", outroAtendimentoId);
```

Ao entrar, o cliente recebe a lista inicial. Quando `Mensagem/Criar` ou
`Mensagem/Atualizar` salva uma mensagem, os clientes inscritos naquele
atendimento recebem `MensagensAtualizadas(atendimentoId, mensagens)` com a
lista completa no mesmo formato do GET. Substitua a lista na tela a cada evento.
Falhas na conexão inicial devem ser tratadas pelo frontend; a reconexão automática
é aplicada depois que a conexão foi estabelecida.

O CORS permite o frontend em `http://localhost:3000` com credenciais.
O projeto não implementa autenticação: a inscrição valida a existência do
atendimento, mas não verifica permissões por usuário.
Um GET via curl retorna uma resposta única; ele não se torna uma assinatura
SignalR. Alterações feitas diretamente no banco não disparam eventos.
