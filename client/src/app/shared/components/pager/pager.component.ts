import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnChanges {
  @Input() totalCount: number = 0;
  @Input() pageSize: number = 5;
  @Input() pageNumber: number = 1;
  @Output() pageChanged = new EventEmitter<number>();

  totalPages: number = 0;
  pages: number[] = [];

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['totalCount'] || changes['pageSize'] || changes['pageNumber']) {
      this.totalPages = Math.ceil(this.totalCount / this.pageSize);
      this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);
    }
  }

  changePage(page: number) {
    if (page >= 1 && page <= this.totalPages && page !== this.pageNumber) {
      this.pageChanged.emit(page);
    }
  }

  next() {
    if (this.pageNumber < this.totalPages) {
      this.pageChanged.emit(this.pageNumber + 1);
    }
  }

  previous() {
    if (this.pageNumber > 1) {
      this.pageChanged.emit(this.pageNumber - 1);
    }
  }
}
