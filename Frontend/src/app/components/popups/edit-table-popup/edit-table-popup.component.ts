import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ITable } from 'src/app/models/table';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { TableService } from 'src/app/services/table.service';
import { AttributeService } from 'src/app/services/attribute.service';
import { ConnectionService } from 'src/app/services/connection.service';

@Component({
  selector: 'app-edit-table-popup',
  templateUrl: './edit-table-popup.component.html',
  styleUrls: ['./edit-table-popup.component.css']
})
export class EditTablePopupComponent {

  @Output() attributeDeleted = new EventEmitter<number>();
  @Output() tableDeleted = new EventEmitter<number>();

  table! : ITable; 
  attributes!: FormArray;

  schemeId! : string;

  dataTypes: string[] = [
    "int",
    "bigint",
    "boolean",
    "decimal",
    "double precision",
    "float",
    "text",
    "char",
    "date",
    "timestamp"
  ];

  constructor(@Inject(MAT_DIALOG_DATA) private dialogData: any,
    private dialogRef: MatDialogRef<EditTablePopupComponent>,
    private formBuilder: FormBuilder,
    private tableService: TableService,
    private attributeService: AttributeService,
    private connectionService: ConnectionService,
    private snackbar: SnackbarService) {
    
      this.schemeId = dialogData.schemeId;
      this.table = dialogData.table;

      this.table.attributes.map(a => { 
        this.attributesInputs.push(this.formBuilder.group({
          name: a.name,
          type: a.dataTypeId
        })) 
      });

      this.updateTableForm.controls.tableName.setValue(this.table.name);
  }

  get attributesInputs() {
    return this.updateTableForm.controls.attributesInputs as FormArray;
  }


  updateTableForm = this.formBuilder.group({
    tableName: this.formBuilder.control('', Validators.required),
    attributesInputs: this.formBuilder.array([]),
  });

  addAttribute() {
    let newAttribute = {
      dataTypeId: 1
    };
    this.attributeService.createAttribute(this.schemeId, this.table!.id, newAttribute).subscribe({
      next: (attribute) => {
        this.table.attributes.push(attribute);
        this.attributesInputs.push(this.formBuilder.group({
          name: attribute.name,
          type: attribute.dataTypeId
        }));
        this.snackbar.showMessage("Додано атрибут!");
      }
    });
  }

  deleteAttribute(index: number) {
    let id = this.table!.attributes[index].id;
    this.attributeService.deleteAttribute(this.schemeId, this.table!.id, id).subscribe({
      next: () => {
        this.table.attributes = this.table!.attributes.filter(a => a.id != id);
        this.attributesInputs.removeAt(index);

        this.attributeDeleted.emit(id);
        this.snackbar.showMessage("Атрибут успішно видалено!");
      },
      error: () => {
        this.snackbar.showMessage("Сталася помилка при видаленні атрибуту!");
      }
    })
  }

  deleteAttributeConnections(index: number) {
    let attribute = this.table!.attributes[index];
    this.connectionService.deleteAttributeConnections(this.schemeId, this.table!.id, attribute.id).subscribe({
      next: () => {
        attribute.connectionsTo = [];
        this.snackbar.showMessage("Успішно видалено з'єднання!");
      },
      error: () => {
        this.snackbar.showMessage("Сталася помилка при видаленні зв'язків атрибуту!");
      }
    });
  }

  updateTable() {
    if (this.updateTableForm.valid) {
      let patchingTable = {
        attributes: this.table.attributes,
        name: this.updateTableForm.controls.tableName.value
      }
      
      patchingTable.attributes.map((item, index) => {
        patchingTable.attributes[index].name = this.attributesInputs.value[index].name;
        let typeId = this.attributesInputs.value[index].type;
        patchingTable.attributes[index].dataTypeId = typeId;
        patchingTable.attributes[index].dataType = this.dataTypes[typeId - 1];
      });

      this.tableService.patchTable(patchingTable, this.table!.id, this.schemeId).subscribe({
        next: () => {
          this.snackbar.showMessage("Успішно оновлено таблицю!");
          this.table!.attributes = patchingTable.attributes;
          this.table!.name = patchingTable.name!;
        },
        error: () => {
          this.snackbar.showMessage("Виникла помилка при оновленні таблиці!");
        }
      });
    }
  }

  deleteTable() {
    this.tableService.deleteTable(this.table.id, this.schemeId).subscribe({
      next: () => {
        this.tableDeleted.emit(this.table.id);
        this.snackbar.showMessage("Успішно видалено таблицю!");
        this.dialogRef.close();
      },
      error: () => {
        this.snackbar.showMessage("Виникла помилка при видаленні таблиці!");
      }
    });
  }
}