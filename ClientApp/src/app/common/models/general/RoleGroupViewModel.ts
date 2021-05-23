import { Group } from "./Group";
import { RoleViewModel } from "./RoleViewModel";

export interface RoleGroupViewModel{
    roles: RoleViewModel[];
    groups: Group[];
}