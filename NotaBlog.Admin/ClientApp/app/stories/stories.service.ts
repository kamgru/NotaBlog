import { Injectable } from '@angular/core';
import { IStoryHeader } from './models/IStoryHeader';
import { IPaginatedData } from  '../shared/models/IPaginatedData';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { IStory } from './models/IStory';
import { HttpClient, HttpParams } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable()
export class StoriesService {

    constructor(private http: HttpClient) {}

    public getStoryHeaders(page:number, count:number): Observable<IPaginatedData<IStoryHeader>> {

        const params = new HttpParams()
            .set('page', page.toString())
            .set('count', count.toString());

        return this.http.get<IPaginatedData<IStoryHeader>>('/api/stories', {params});
    }

    public getStory(id:string): Observable<IStory> {
        return this.http.get<IStory>(`/api/stories/${id}`)
    }
}