var store = new Vuex.Store({
    strict: true,
    state: {
        posts: null,
        currentPost: null,
        createPostVisible: false,
        createPostErrors: [],
        editPostVisible: false,
        editPostErrors: []
    },
    mutations: {
        setCreatePostVisible: (state, value) => {
            state.createPostVisible = value;
        },
        setEditPostVisible: (state, value) => {
            state.editPostVisible = value;
        },
        setCreatePostErrors: (state, value) => {
            state.createPostErrors = value;
        },
        setEditPostErrors: (state, value) => {
            state.editPostErrors = value;
        },
        setCurrentVersion: (state, payload) => {
            let post = state.posts[payload.postId];
            post.currentVersion = post.postVersions[payload.currentVersion];
        },
        setCurrentPost: (state, value) => {
            state.currentPost = state.posts[value];
        },
        toggleCreatePostVisible: (state) => {
            state.createPostVisible = !state.createPostVisible;
        },
        toggleEditPostVisible: (state) => {
            state.editPostVisible = !state.editPostVisible;
        },
        setPosts: (state, value) => {
            state.posts = value;
        },
        addPost: (state, value) => {
            state.posts[value.postId] = value;
        },
        addPostVersion: (state, value) => {
            state.posts[value.postId].postVersions.push(value);
        },
        setPublished: (state, value) => {
            state.currentPost.published = value;
        }
    },
    actions: {
        init: (context) => {
            context.commit('setPosts', model);
        },
        createPost: (context, payload) => {
            context.commit('setCreatePostErrors', []);
            $.ajax({
                type: "post",
                url: "/api/Post/Post",
                data: JSON.stringify(payload),
                contentType: "application/json",
                dataType: "json",
                headers: { "Authorization": "Bearer " + localStorage.getItem("jwt") },
                success: function (result) {
                    context.commit('setCreatePostVisible', false);
                    context.commit('addPost', result);
                },
                error: function (result) {
                    context.commit('setCreatePostErrors', parseErrors(result));
                }
            });
        },
        createPostVersion: (context, payload) => {
            context.commit('setEditPostErrors', []);
            $.ajax({
                type: "post",
                url: "/api/Post/PostVersion",
                data: JSON.stringify(payload),
                contentType: "application/json",
                dataType: "json",
                headers: { "Authorization": "Bearer " + localStorage.getItem("jwt") },
                success: function (result) {
                    context.commit('addPostVersion', result)
                },
                error: function (result) {
                    context.commit('setEditPostErrors', parseErrors(result));
                }
            });
        },
        setCurrentVersion: (context, payload) => {
            context.commit('setEditPostErrors', []);
            $.ajax({
                type: "put",
                url: "/api/Post/Post",
                data: JSON.stringify(payload),
                contentType: "application/json",
                dataType: "json",
                headers: { "Authorization": "Bearer " + localStorage.getItem("jwt") },
                success: function (result) {
                    context.commit('setCurrentVersion', payload);
                },
                error: function (result) {
                    context.commit('setEditPostErrors', parseErrors(result));
                }
            });
        },
        togglePublish: (context, payload) => {
            context.commit('setEditPostErrors', []);
            $.ajax({
                type: "put",
                url: "/api/Post/Post",
                data: JSON.stringify(payload),
                contentType: "application/json",
                dataType: "json",
                headers: { "Authorization": "Bearer " + localStorage.getItem("jwt") },
                success: function (result) {
                    context.commit('setPublished', result.published);
                },
                error: function (result) {
                    context.commit('setEditPostErrors', parseErrors(result));
                }
            });
        }
    }
});