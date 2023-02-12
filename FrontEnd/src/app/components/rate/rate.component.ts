import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { OffersService } from 'src/app/services/offers.service';
import { RatingsService } from 'src/app/services/ratings.service';

@Component({
  selector: 'app-rate',
  templateUrl: './rate.component.html',
  styleUrls: ['./rate.component.scss']
})
export class RateComponent implements OnInit {

  acceptedOffers = [];

  public starForm: FormGroup = new FormGroup({
    nrStars: new FormControl('', [Validators.required])
  });


  constructor(private offersService: OffersService,
    private ratingsService: RatingsService, private router: Router) { }

  get nrStars(): AbstractControl | null {
    return this.starForm.get('nrStars');
  }


  ngOnInit(): void {
    this.offersService.getAcceptedOffers().subscribe({
      next: (data) => {
        this.acceptedOffers = data;
      },
      error: (err) => {
        console.error(err);
      }
    })
  }

  submitRating(id: any) {
    this.ratingsService.send_rating(this.starForm.value, id).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (err) => {
        console.error(err);
      }
    })
  }
}
