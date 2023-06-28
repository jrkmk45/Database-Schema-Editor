import { ChangeDetectorRef, Component, ElementRef, EventEmitter, HostListener, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { ITable } from 'src/app/models/table';
import { ConnectionService } from 'src/app/services/connection.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { TableService } from 'src/app/services/table.service';
import { EditTablePopupComponent } from '../popups/edit-table-popup/edit-table-popup.component';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent {

  @ViewChild(EditTablePopupComponent) editTablePopupComponent?: EditTablePopupComponent;

  @Input() table?: ITable;
  @Input() index? : number;

  @Output() tableMoved = new EventEmitter<{ x: number, y: number, index: number }>();

  @Output() tableDeleted = new EventEmitter<number>();

  @Output() connectionCreated = new EventEmitter();
  @Output() creatingConnectionFromAttributeId = new EventEmitter<{ attributeId: number, tableId: number }>();

  @Output() attributeRemoved = new EventEmitter<number>();

  @Output() resetConnectionCreation = new EventEmitter();

  
  private isMousePressed = false;
  private startPoint?: { x: number, y: number };
  private isTableMoved = false;

  isSelected = false;

  private canvasSize = 5000;

  private isCreatingConnection = false;
  @Input() connectionFromAttributeId? : number | undefined = undefined;

  constructor(private tableService: TableService,
    private connectionService : ConnectionService,
    private snackbar: SnackbarService,
    private route: ActivatedRoute,
    private elementRef: ElementRef,
    private dialog: MatDialog) {}

  onMouseUp() {
    this.isMousePressed = false;
    if (!this.isTableMoved ) {
      return;
    }

    this.isTableMoved = false;
    
    let schemeId = this.route.snapshot.paramMap.get('id')!;

    let table = {
      x: this.table!.x,
      y: this.table!.y
    }

    this.tableService.patchTable(table, this.table!.id, schemeId).subscribe();
  }

  onModifyTableClick() {

    let schemeId = this.route.snapshot.paramMap.get('id')!;
    const dialogConfig = new MatDialogConfig();
    dialogConfig.panelClass = 'popup-window-z-index';
    dialogConfig.data = {
      schemeId: schemeId,
      table: this.table
    }
    
    let dialogRef: MatDialogRef<EditTablePopupComponent> = this.dialog.open(EditTablePopupComponent, dialogConfig);

    dialogRef.componentInstance.attributeDeleted.subscribe((id: number) => {
      this.table!.attributes = this.table?.attributes.filter(t => t.id != id)!;
      this.attributeRemoved.emit(id);
    })

    dialogRef.componentInstance.tableDeleted.subscribe((id: number) => {
      this.tableDeleted.emit(id);
    })

  }

  onConnectionCreatorMouseDown(event : number) {
    this.isCreatingConnection = true;
    this.creatingConnectionFromAttributeId.emit({
      attributeId: event,
      tableId: this.table!.id
    });
    this.connectionFromAttributeId = event;
  }

  onAttributeMouseUp(event : number) {
    if (this.connectionFromAttributeId) {
      this.createConnection(this.connectionFromAttributeId, event);
    }
  }

  createConnection(idFrom : number, idTo : number) {
    this.isCreatingConnection = false;
    let connection = {
      connectionType: 0,
      attributeFromId: idFrom,
      attributeToId: idTo,
    };
    let schemeId = this.route.snapshot.paramMap.get('id')!;
    this.connectionService.createConnectionAsync(schemeId, this.table!.id, idFrom, connection).subscribe({
      next: (connection) => {
        this.connectionCreated.emit(connection);
        this.snackbar.showMessage("Успішно створено з'єднання!");
      },
      error: () => {
        this.snackbar.showMessage("Виникла помилка при створенні з'єднання");
      }
    });
  }

  @HostListener('document:click', ['$event.target'])
  onMouseClick(event : any) {
    let clickedInside = this.elementRef.nativeElement.contains(event);
    if (!clickedInside) {
      this.isSelected = false;
    }
  }

  onMouseDown(event : MouseEvent) {
    this.isSelected = true;
    this.isMousePressed = true;
    this.startPoint = this.getCursorPosition(event);
  }
  
  @HostListener('document:mousemove', ['$event'])
  moveTable(event : MouseEvent) { 
    if (!this.isMousePressed || this.isCreatingConnection) {
      return;
    }

    let offsetX = event.clientX - this.startPoint!.x;
    let offsetY = event.clientY - this.startPoint!.y;

    if (this.table!.x + offsetX < 0 || this.table!.x + offsetX > this.canvasSize ||
      this.table!.y + offsetY < 0 ||  this.table!.y + offsetY > this.canvasSize) {
      return;
    }
    
    this.table!.x += offsetX;
    this.table!.y += offsetY;

    this.startPoint = this.getCursorPosition(event);

    this.isTableMoved = true;
  }
  
  @HostListener('document:mouseup', ['$event'])
  onDocumentMouseUp(event: MouseEvent): void {  
    console.log("onDocumentMouseUp");
    
    if (this.isCreatingConnection) {
      this.isCreatingConnection = false;
    }

    this.onMouseUp();
    this.resetConnectionCreation.emit();
  }

  private getCursorPosition(event: MouseEvent) {
    return {
      x: event.clientX,
      y: event.clientY
    };
  }
}