import { IAttribute } from "./attribute";

export interface ITable {
  id: number;
  name: string;
  attributes: IAttribute[];
  x: number;
  y: number;

  createdDate: Date;
}