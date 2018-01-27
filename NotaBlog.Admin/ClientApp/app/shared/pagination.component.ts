import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';

type Bounds = {high:number, low:number}; 

@Component({
    selector: 'pagination',
    templateUrl: './pagination.component.html'
})
export class PaginationComponent implements OnChanges {
    
    @Input() pageSize:number = 0;
    @Input() totalItems:number = 0;
    @Output() onPageChange = new EventEmitter();

    totalPages:number = 0;
    visiblePages:number = 5;
    boundSize:number = 2;
    pageIndexCollection:number[];
    currentPage:number = 1;
    
    ngOnChanges(changes: SimpleChanges): void {
        this.totalPages = Math.ceil(this.totalItems / this.pageSize);
        this.updatePagination(1);
    }
    
    private updatePagination(page:number): void{
        this.currentPage = this.clamp(page, 1, this.totalPages);
        this.pageIndexCollection = this.calculatePageIndices(this.currentPage);
    }

    public pageChange(page:number): void {
        this.updatePagination(page);
        this.onPageChange.emit(this.currentPage);
    }

    private clamp(num:number, min:number, max:number): number {
        return Math.min(Math.max(num, min), max);
    }

    private calculatePageIndices(page: number): number[] {
        const bounds = this.calculateBounds(this.currentPage);
        return page == 1 
            ? this.range(1, Math.min(bounds.high, this.visiblePages, this.totalPages) + 1)
            : this.pageIndexCollection = this.range(bounds.low, Math.min(bounds.high, this.totalPages) + 1);
    }

    private calculateBounds(page:number):Bounds {
        return {
            low: Math.max(1, page - this.boundSize),
            high: Math.min(page + this.boundSize, this.totalPages)
        };
    }

    private range(start:number, end:number):number[] {
        let result:number[] = new Array(end - start);
        for (let i = 0; i < end - start; i++) {
            result[i] = i + start;
        }
        return result;
    }
}