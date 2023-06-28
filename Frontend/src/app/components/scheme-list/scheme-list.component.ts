import { Component, Input } from '@angular/core';
import { ISchemeListItem } from 'src/app/models/scheme-list-item';
import { SchemeService } from 'src/app/services/scheme.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
  selector: 'app-scheme-list',
  templateUrl: './scheme-list.component.html',
  styleUrls: ['./scheme-list.component.css']
})
export class SchemeListComponent {

  @Input() schemes?: ISchemeListItem[];

  constructor(private schemeService: SchemeService,
    private snackbar: SnackbarService) { }

  deleteScheme(i: number) {
    let id = this.schemes![i].id;
    this.schemeService.deleteScheme(id).subscribe({
      next: () => {
        this.schemes = this.schemes?.filter(c => c.id != id);
        this.snackbar.showMessage("Успішно видалено схему!");
      },
      error: () => {
        this.snackbar.showMessage("Виникла помилка при видаленні схеми!");
      }
    })
  }
}
