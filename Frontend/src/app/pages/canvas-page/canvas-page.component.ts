import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateTablePopupComponent } from 'src/app/components/popups/create-table-popup/create-table-popup.component';
import { IScheme } from 'src/app/models/scheme';
import { ITable } from 'src/app/models/table';
import { SchemeService } from 'src/app/services/scheme.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
  selector: 'app-canvas-page',
  templateUrl: './canvas-page.component.html',
  styleUrls: ['./canvas-page.component.css']
})
export class CanvasPageComponent implements OnInit {

  scheme?: IScheme;
  isLoading = true;

  constructor(private dialog: MatDialog, 
    private route: ActivatedRoute,
    private router: Router,
    private schemeService: SchemeService,
    private snackbar: SnackbarService) {}

  ngOnInit() {
    let schemeId = this.route.snapshot.paramMap.get('id')!;
    this.schemeService.getScheme(schemeId).subscribe({
      next: (scheme) => {
        this.scheme = scheme;
        this.isLoading = false;
      },
      error: (error: HttpErrorResponse) => {
        if (error.status == 403) {
          this.snackbar.showMessage("У вас немає доступу до даного ресурсу!");
          this.router.navigate(['']);
          return;
        }
        this.snackbar.showMessage("Виникла помилка при завантаженні схеми.");
        this.isLoading = false;
      }
    })
  }

  onAddTableClick(event : MouseEvent) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.panelClass = 'popup-window-z-index';
    dialogConfig.data = {
      schemeId: this.scheme?.id,
      x: event.clientX,
      y: event.clientY
    };

    const dialogRef: MatDialogRef<CreateTablePopupComponent> = this.dialog.open(CreateTablePopupComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((table : ITable) => {
      if (table != null) {
        this.scheme?.tables.push(table);
      }
    });
  }
}
