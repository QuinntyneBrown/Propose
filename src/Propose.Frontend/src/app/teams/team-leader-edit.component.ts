import { TeamLeader } from "./team-leader.model";
import { TeamLeaderService } from "./team-leader.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";

const template = require("./team-leader-edit.component.html");
const styles = require("./team-leader-edit.component.scss");

export class TeamLeaderEditComponent extends HTMLElement {
    constructor(
        private _teamLeaderService: TeamLeaderService = TeamLeaderService.Instance,
        private _router: Router = Router.Instance
        ) {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onTitleClick = this.onTitleClick.bind(this);
    }

    static get observedAttributes() {
        return ["team-leader-id"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.teamLeaderId ? "Edit TeamLeader": "Create TeamLeader";

        if (this.teamLeaderId) {
            const teamLeader: TeamLeader = await this._teamLeaderService.getById(this.teamLeaderId);                
            this._nameInputElement.value = teamLeader.name;  
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
        const teamLeader = {
            id: this.teamLeaderId,
            name: this._nameInputElement.value
        } as TeamLeader;
        
        await this._teamLeaderService.add(teamLeader);
        this._router.navigate(["team-leader","list"]);
    }

    public async onDelete() {        
        await this._teamLeaderService.remove({ id: this.teamLeaderId });
        this._router.navigate(["team-leader","list"]);
    }

    public onTitleClick() {
        this._router.navigate(["team-leader", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "team-leader-id":
                this.teamLeaderId = newValue;
                break;
        }        
    }

    public teamLeaderId: number;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".team-leader-name") as HTMLInputElement;}
}

customElements.define(`ce-team-leader-edit`,TeamLeaderEditComponent);
