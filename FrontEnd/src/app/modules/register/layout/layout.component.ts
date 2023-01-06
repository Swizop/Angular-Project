import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  registerAs = '';
  constructor(private data: DataService) {}

  ngOnInit(): void {
    this.data.currentRegisterAs.subscribe(message => this.registerAs = message)
  }
}
