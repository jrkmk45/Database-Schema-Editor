import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ISchemeListItem } from 'src/app/models/scheme-list-item';
import { SchemeService } from 'src/app/services/scheme.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
  selector: 'app-scheme-list-page',
  templateUrl: './scheme-list-page.component.html',
  styleUrls: ['./scheme-list-page.component.css']
})
export class SchemeListPageComponent implements OnInit {

  schemes?: ISchemeListItem[];

  constructor(private schemeService: SchemeService,
    private snackbar: SnackbarService) {}

  ngOnInit() {
    this.schemeService.getUserSchemes().subscribe({
      next: (schemes) => {
        this.schemes = schemes;
        console.log(this.schemes);
      },
      error: () => {
        this.snackbar.showMessage("Виникла помилка при завантаженні схем!");
      }
    })
  }

  onSchemeAdded(event: ISchemeListItem) {
    this.schemes!.push(event);
  }
}
