import { Group } from '../general/Group';
import { RoleViewModel } from '../general/RoleViewModel';
import { Role } from './role';

export interface AuthenticationResponse {
    token: string;
    userId: string;
    userName: string;
    redirectUrl: string;
    roles: RoleViewModel[];
    name: string;
    surName: string;
    group: Group;
    password: string;
}
