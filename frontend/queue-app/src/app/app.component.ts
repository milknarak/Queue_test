
import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  template: `
    <div class="header"></div>
    <div class="container">
      <div class="card">
        <div class="queue">{{queue}}</div>
        <button (click)="next()">รับบัตรคิว</button>
        <button class="gray" (click)="reset()">ล้างคิว</button>
      </div>
    </div>
  `
})
export class AppComponent {
  queue = '--';
  api = 'http://localhost:5000/api/queue';

  constructor(private http: HttpClient) {}

  next() {
    this.http.post(this.api + '/next', {}, { responseType: 'text' })
      .subscribe(q => this.queue = q);
  }

  reset() {
    this.http.post(this.api + '/reset', {}, { responseType: 'text' })
      .subscribe(q => this.queue = q);
  }
}