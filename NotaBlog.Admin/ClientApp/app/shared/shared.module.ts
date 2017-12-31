import { NgModule } from '@angular/core';
import { CommonModule, NgClass } from '@angular/common';
import { PaginationComponent } from './pagination.component';

@NgModule({
    imports: [
        CommonModule
    ],
    exports: [
        PaginationComponent
    ],
    declarations: [
        PaginationComponent
    ],
    providers: [

    ]
})
export class SharedModule {}