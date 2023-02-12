import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { OffersService } from 'src/app/services/offers.service';

@Component({
  selector: 'app-offer',
  templateUrl: './offer.component.html',
  styleUrls: ['./offer.component.scss']
})
export class OfferComponent implements OnInit {
  @Input()
  project!: { [x: string]: any; };

  @Output() finalizeOfferEvent = new EventEmitter<boolean>();

  errNumber = 0;
  public offerForm: FormGroup = new FormGroup({
    minBound: new FormControl('', [Validators.required]),
    maxBound: new FormControl('', [Validators.required]),
    date: new FormControl('', [Validators.required]),
  });

  constructor(private offers: OffersService) { }

  get minBound(): AbstractControl | null {
    return this.offerForm.get('title');
  }

  get maxBound(): AbstractControl | null {
    return this.offerForm.get('description');
  }

  get date(): AbstractControl | null {
    return this.offerForm.get('location');
  }

  ngOnInit(): void {
  }

  closeOffer() {
    this.finalizeOfferEvent.emit(false);
  }
  submitOffer() {
    if (this.offerForm.invalid) {
      this.errNumber = 1;
    }
    else {
      this.offerForm.addControl('projectId', new FormControl(this.project['id']));
      // trb sa adaugam manual idul proiectului

      console.log(this.offerForm.value);
      this.offers.new_offer(this.offerForm.value).subscribe({
        next: () => {
          console.log("Succes!");
          this.errNumber = 0;
          this.finalizeOfferEvent.emit(false);
        },
        error: err => {
          console.error(err.error);
          this.errNumber = 2;
        }
      })
    }
  }
}
