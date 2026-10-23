# Mensagens em tempo real

As mensagens do atendimento são atualizadas em tempo real com o SignalR. O frontend se conecta ao hub da API para receber o histórico e acompanhar novas mensagens, sem precisar atualizar a página.

## Endereços da API

- **Histórico de mensagens:** `http://localhost:5276/Mensagem/ListarPorAtendimento/6`
- **Hub SignalR:** `http://localhost:5276/hubs/mensagens`

O número `6` representa o ID do atendimento usado neste exemplo.

## Instalação

Na pasta do frontend, execute:

```bash
npm install @microsoft/signalr
```

## Exemplo de conexão

```javascript
import { HubConnectionBuilder } from "@microsoft/signalr";

const atendimentoId = 6;

const conexao = new HubConnectionBuilder()
  .withUrl("http://localhost:5276/hubs/mensagens")
  .withAutomaticReconnect()
  .build();

conexao.on("MensagensAtualizadas", (id, mensagens) => {
  if (id === atendimentoId) {
    console.log(mensagens);

    // No React, atualize o estado da tela:
    // setMensagens(mensagens);
  }
});

conexao.onreconnected(async () => {
  try {
    await conexao.invoke("EntrarAtendimento", atendimentoId);
  } catch (erro) {
    console.error("Erro ao reconectar ao atendimento:", erro);
  }
});

await conexao.start();
await conexao.invoke("EntrarAtendimento", atendimentoId);
```

Ao sair da tela, encerre a conexão:

```javascript
await conexao.stop();
```

Para trocar de atendimento usando a mesma conexão:

```javascript
await conexao.invoke("SairAtendimento", atendimentoId);
await conexao.invoke("EntrarAtendimento", outroAtendimentoId);
```

## Funcionamento

Ao entrar em um atendimento, o cliente recebe o histórico de mensagens. Quando uma mensagem é salva pelas rotas `Mensagem/Criar` ou `Mensagem/Atualizar`, a API envia o evento `MensagensAtualizadas` aos clientes conectados àquele atendimento.

O evento contém o ID do atendimento e a lista completa de mensagens, no mesmo formato retornado pelo GET. No frontend, essa lista deve substituir as mensagens exibidas na tela.

### Reconexão no React

O hook `useMensagensTempoReal` tenta conectar novamente após 3 segundos quando a conexão inicial ou a entrada no atendimento falha, ou quando as tentativas automáticas do SignalR se esgotam.

Após reconectar, o hook entra novamente no grupo do atendimento e recebe o histórico atualizado. Ao sair da tela, as tentativas pendentes são canceladas.

## Configuração e limitações

- O CORS está configurado para permitir o frontend em `http://localhost:3000`, com credenciais.
- O projeto ainda não possui autenticação. A entrada no grupo verifica se o atendimento existe, mas não valida permissões por usuário.
- A consulta GET retorna o histórico apenas no momento da requisição. Para receber atualizações em tempo real, é necessário conectar ao hub.
- Alterações feitas diretamente no banco de dados não disparam eventos do SignalR.