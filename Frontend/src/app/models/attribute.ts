import { IConnection } from "./connection";

export interface IAttribute {
  id: number;
  name: string;
  dataType: string;
  dataTypeId: number;
  connectionsTo: IConnection[];
  connectionsFrom: IConnection[];
}