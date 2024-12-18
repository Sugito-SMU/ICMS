import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-smu-loader',
  templateUrl: './smu-loader.component.html',
  styleUrls: ['./smu-loader.component.css']
})
export class SmuLoaderComponent implements OnInit {
    @Input() loaderClass: string;

  constructor() { }

  ngOnInit() {
  }

}
