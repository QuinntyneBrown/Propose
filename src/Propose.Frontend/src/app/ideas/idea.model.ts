import { User } from "../users";

export class Vote {

}

export class Idea { 
    public id: any;
    public ideationId: any;
    public userId: any;
    public name: string;
    public htmlBody: string;
    public htmlDescription: string;
    public user: User;
    public votes:Array<Vote> = []
}

