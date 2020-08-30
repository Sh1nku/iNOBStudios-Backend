Vue.component('edit-post-modal', {
    computed: {
        errors() {
            return this.$store.state.editPostErrors;
        },
        post() {
            return this.$store.state.currentPost;
        }
    },
    data() {
        return {
            title: ""
        }
    },
    methods: {
        hide: function () {
            this.$store.commit('toggleEditPostVisible');
        },
        submit: function () {
            let payload = { title: this.title };
            this.$store.dispatch('editPost', payload);
        },
        createPostVersion: function () {
            let payload = { title: this.title, postId: this.post.postId };
            this.$store.dispatch('createPostVersion', payload);
        },
        makeCurrent: function (versionId) {
            let payload = { postId: this.post.postId, currentVersion: versionId };
            this.$store.dispatch('setCurrentVersion', payload);
        },
    },
    template:
        `
<div class="backgroundStyle" @mousedown.self="hide">
    <div class="modalStyle">
        <span class="closeStyle" @click="hide">&times;</span>
        <h1>Edit Post</h1>
        <div v-if="errors">
            <ul v-for="error in errors">
                <li style="color: red">{{error}}</li>
            </ul>
        </div>
        <table class="table">
            <tr>
                <th>Date</th>
                <th>Title</th>
                <th>Current</th>
                <th>Edit</th>
            </tr>
            <tr v-for="version in post.postVersions">
                <td>{{getDateFromJSON(version.postedDate)}}</td>
                <td>{{version.title}}</td>
                <td v-if="post.currentVersion.postVersionId == version.postVersionId">True</td>
                <td v-else><button @click="makeCurrent(version.postVersionId)">Make Current</button></td>
                <td><button>Edit</button></td>
            </tr>
        </table>
        <div>
            <input type="text" v-model="title" />
            <button class="btn btn-primary" @click="createPostVersion">Add Post Version</button>
        </div>
        <button type="button" class="btn btn-primary" @click="submit">Submit</button>
        <button type="button" class="btn btn-primary" @click="hide">Cancel</button>
    </div>
</div>
`

});