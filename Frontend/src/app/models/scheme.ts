import { ITable } from "./table";

export interface IScheme {
  id: number;
  name: string;
  tables: ITable[];
  createdDate: Date;
}