import { User } from "../users";
import { DigitalAsset } from "../digital-assets";
import { Link } from "../links";
import { Vote } from "./vote.model";

export class Idea { 
    public id: any;
    public ideationId: any;
    public userId: any;
    public name: string;
    public htmlBody: string;
    public htmlDescription: string;
    public user: User;
    public votes: Array<Vote> = [];
    public digitalAssets: Array<DigitalAsset> = [];
    public links: Array<Link> = [];
}

