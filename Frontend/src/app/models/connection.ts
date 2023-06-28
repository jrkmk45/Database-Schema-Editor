import { ConnectionType } from "../enums/connection-type";

export interface IConnection {
  id: number;
  connectionType: ConnectionType;
  attributeFromId: number;
  attributeToId: number;
}