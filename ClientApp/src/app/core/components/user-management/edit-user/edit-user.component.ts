import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/common/models/authentication/User';
import { RoleViewModel } from 'src/app/common/models/general/RoleViewModel';
import { environment } from 'src/environments/environment';
import { cloneDeep } from 'lodash';
import { RoleGroupViewModel } from 'src/app/common/models/general/RoleGroupViewModel';
import { Group } from 'src/app/common/models/general/Group';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent implements OnInit {
  userViewModel: User;
  userModelId: number;
  editedUserModel: User;
  edit_name_surName: string;
  currencies: User[];
  roles: RoleViewModel[];
  groups: Group[];
  selectedGroup: string;
  selectedRoles: string[] = [];
  editUserFormGroup: FormGroup;

  constructor(private http: HttpClient,
    private activatedRoute: ActivatedRoute,
    private router: Router) { }

  async ngOnInit() {
    this.userModelId = parseInt(this.activatedRoute.snapshot.params.userId);
    await this.getUser(this.userModelId);
    await this.getRolesAndGroups();
  }

  async getUser(userId: number) {
    if (userId !== -1) {
      this.userViewModel = await this.http.get<User>(environment.apiUrl + 'user/' + userId).toPromise();
    }
    else {
      this.userViewModel = undefined;
      this.userViewModel = {
        name: '',
        userId: -1,
        userName: '',
        surname: '',
        password: '',
        roles: [],
        accessToken: '',
        group: {
          groupName: '',
          groupId: null
        }
      };
    }

    this.editedUserModel = cloneDeep(this.userViewModel);

    this.edit_name_surName = this.editedUserModel.name + " " + this.editedUserModel.surname;
    this.selectedRoles = [];
    this.editedUserModel.roles.forEach(role => {
      this.selectedRoles.push(role.name);
    });
    this.selectedGroup = this.editedUserModel.group.groupName;

    this.editUserFormGroup = new FormGroup({
      name_surname: new FormControl(this.edit_name_surName, Validators.required),
      userName: new FormControl(this.editedUserModel.userName, Validators.required),
      password: new FormControl(this.editedUserModel.password, Validators.required),
      accessToken: new FormControl(this.editedUserModel.accessToken, Validators.required),
      group: new FormControl(this.selectedGroup, Validators.required),
      roles: new FormControl(this.selectedRoles, Validators.required)
    });
  }

  changeName_surName() {
    let name_surName = this.edit_name_surName.trim().split(" ");
    this.editedUserModel.name = name_surName[0];
    this.editedUserModel.surname = name_surName[1];
  }

  async getRolesAndGroups(){
    let rolesGroups = await this.http.get<RoleGroupViewModel>(environment.apiUrl + 'group/getRoleGroupModel').toPromise();
    this.roles = rolesGroups.roles;
    this.groups = rolesGroups.groups;
  }

  async updateUser() {
    if (confirm(this.userExists() ? "Ви справді хочете змінити дані користувача " + this.userViewModel.name + " " + this.userViewModel.surname + "?" : "Ви справді хочете додати користувача " + this.editedUserModel.name + " " + this.editedUserModel.surname + "?")) {
      this.editedUserModel.userName = this.editUserFormGroup.value.userName;
      this.editedUserModel.password = this.editUserFormGroup.value.password;
      this.editedUserModel.accessToken = this.editUserFormGroup.value.accessToken;
      this.editedUserModel.group.groupId = +this.editUserFormGroup.value.group;
      let editedRoles: RoleViewModel[] = [];
      this.selectedRoles.forEach(role => {
        let newRole = {
          roleId: this.roles.find(x => x.name === role).roleId,
          name: role
        };
        editedRoles.push((newRole));
      });
      if (editedRoles) {
        this.editedUserModel.roles = editedRoles;
      }
      this.editedUserModel.group = this.groups.find(x => x.groupName === this.selectedGroup);

      this.userExists() ? await this.updateUserRequest('updateUser') : await this.updateUserRequest('addUser');
    }
  }

  cancel() {
    if (confirm('Скасувати зміни?')) {
      this.getUser(this.userModelId);
    }
  }

  userExists(): boolean {
    return this.userModelId !== -1;
  }

  async updateUserRequest(route: string) {
    await this.http.post<User>(environment.apiUrl + 'user/' + route, this.editedUserModel).toPromise()
      .then(async userModel => {
        this.editedUserModel = userModel;
        if (!this.userExists()) {
          let users = await this.http.get<User[]>(environment.apiUrl + 'user/').toPromise();
          this.userModelId = users.find(x => x.userName === this.editedUserModel.userName).userId;
          this.router.navigate(['edit-user', { userId: this.userModelId }]);

          this.getUser(this.userModelId);
        }
      }).catch(error => {
        console.error(error);
      });
  }
}
