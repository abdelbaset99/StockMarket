import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

export function createHubConnection(): HubConnection {
  const builder = new HubConnectionBuilder();
  const connection = builder.withUrl('http://localhost:5089/stockhub').build();
  return connection;
}
