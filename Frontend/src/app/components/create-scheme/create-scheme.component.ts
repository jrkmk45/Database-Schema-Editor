import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ISchemeListItem } from 'src/app/models/scheme-list-item';
import { SchemeService } from 'src/app/services/scheme.service';
import { SnackbarService } from 'src/app/services/snackbar.service';

@Component({
  selector: 'app-create-scheme',
  templateUrl: './create-scheme.component.html',
  styleUrls: ['./create-scheme.component.css']
})
export class CreateSchemeComponent {
  
  @Output() schemeAdded = new EventEmitter<ISchemeListItem>();

  constructor(private formBuilder: FormBuilder,
    private schemeService: SchemeService,
    private snackbar: SnackbarService) {}

  addSchemeForm = this.formBuilder.group({
    name: this.formBuilder.control('', Validators.required)
  });

  onAddscheme() {
    if (this.addSchemeForm.valid) {
      let scheme = {
        name: this.addSchemeForm.value.name
      }

      this.schemeService.createScheme(scheme).subscribe({
        next: (scheme) => {
          this.schemeAdded.emit(scheme);
          this.snackbar.showMessage("Успішно створено схему!");
        },
        error: () => {
          this.snackbar.showMessage("Виникла помилка при створенні схеми!");
        }
      });
    }
  }
}
