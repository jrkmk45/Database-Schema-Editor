import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { TableService } from 'src/app/services/table.service';

@Component({
  selector: 'app-create-table-popup',
  templateUrl: './create-table-popup.component.html',
  styleUrls: ['./create-table-popup.component.css']
})
export class CreateTablePopupComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) private dialogData: any,
    private dialogRef: MatDialogRef<CreateTablePopupComponent>,
    private formBuilder: FormBuilder,
    private tableService: TableService,
    private snackbar: SnackbarService) {
  }
  ngOnInit(): void {
  }

  addTableForm = this.formBuilder.group({
    tableName: this.formBuilder.control('', Validators.required)
  });
  
  createTable() {
    if (this.addTableForm.valid) {
      var table = {
        name: this.addTableForm.value.tableName!,
        x: 150,
        y: 95
      }
      
      this.tableService.createTable(table, this.dialogData.schemeId).subscribe({
        next: (table) => {
          this.snackbar.showMessage("Успішно створено таблицю!");
          this.dialogRef.close(table);
        },
        error: () => {
          this.snackbar.showMessage("Виникла помилка при створенні таблиці!");
        }
      });
    }
  }
}
