import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { OffersService } from 'src/app/services/offers.service';

@Component({
  selector: 'app-project-offers',
  templateUrl: './project-offers.component.html',
  styleUrls: ['./project-offers.component.scss']
})
export class ProjectOffersComponent implements OnInit {

  public subscription: Subscription | undefined;
  public id: any;

  allOffers = [];
  constructor(private route: ActivatedRoute, private offersService: OffersService,
    private router: Router) { }

  ngOnInit(): void {
    this.subscription = this.route.params.subscribe(params => {
      this.id = +params['id'];
      if (this.id) {
        this.getOffers();
      }
    })
  }

  public getOffers() {
    this.offersService.getOffersForProject(this.id).subscribe({
      next: (data) => {
        this.allOffers = data;
      },
      error: (err) => {
        console.error(err);
      }
    })
  }


  editOffer(consId: any) {
    this.offersService.acceptOffer(consId, this.id).subscribe({
      next: () => {
        console.log("Succes!");
        this.router.navigate(['/']);
      },
      error: (err) => { console.error(err) }
    })
  }

  deleteOffer(consId: any) {
    this.offersService.deleteOffer(consId, this.id).subscribe({
      next: () => { console.log("Succes!") },
      error: (err) => { console.error(err) }
    })
  }
}
