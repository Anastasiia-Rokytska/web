import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from 'src/app/common/models/authentication/User';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss']
})
export class UserManagementComponent implements OnInit {
  userModels: User[];
  displayedColumns: string[] = ['userId', 'userName', 'name', 'surname', 'group', 'edit'];
  private userViewModelsSubject: BehaviorSubject<User[]>;
  userViewModels$: Observable<User[]>;
  dataSource = new MatTableDataSource<User>();
  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.userViewModelsSubject = new BehaviorSubject<User[]>([]);
    this.userViewModels$ = this.userViewModelsSubject.asObservable();

    this.http.get<User[]>(environment.apiUrl + 'user').subscribe((data: User[]) => {
      this.userModels = data;
      this.userViewModelsSubject.next(this.userModels);
      this.dataSource.data = this.userModels
    })
  }

  editUser(userId: number) {
    this.router.navigate(['edit-user', { userId: userId }]);
  }
}
