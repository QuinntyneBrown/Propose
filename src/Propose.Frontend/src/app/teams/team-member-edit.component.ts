import { TeamMember } from "./team-member.model";
import { TeamMemberService } from "./team-member.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";

const template = require("./team-member-edit.component.html");
const styles = require("./team-member-edit.component.scss");

export class TeamMemberEditComponent extends HTMLElement {
    constructor(
        private _teamMemberService: TeamMemberService = TeamMemberService.Instance,
        private _router: Router = Router.Instance
        ) {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onTitleClick = this.onTitleClick.bind(this);
    }

    static get observedAttributes() {
        return ["team-member-id"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.teamMemberId ? "Edit TeamMember": "Create TeamMember";

        if (this.teamMemberId) {
            const teamMember: TeamMember = await this._teamMemberService.getById(this.teamMemberId);                
            this._nameInputElement.value = teamMember.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._titleElement.addEventListener("click", this.onTitleClick);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._titleElement.removeEventListener("click", this.onTitleClick);
    }

    public async onSave() {
        const teamMember = {
            id: this.teamMemberId,
            name: this._nameInputElement.value
        } as TeamMember;
        
        await this._teamMemberService.add(teamMember);
        this._router.navigate(["team-member","list"]);
    }

    public async onDelete() {        
        await this._teamMemberService.remove({ id: this.teamMemberId });
        this._router.navigate(["team-member","list"]);
    }

    public onTitleClick() {
        this._router.navigate(["team-member", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "team-member-id":
                this.teamMemberId = newValue;
                break;
        }        
    }

    public teamMemberId: number;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".team-member-name") as HTMLInputElement;}
}

customElements.define(`ce-team-member-edit`,TeamMemberEditComponent);
