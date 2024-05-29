<template>
    <div class="modal">
        <button class="close-button" @click="closeModal">X</button>
        <h2>Sign Up</h2>
        <form class="form" @submit.prevent="newRegistration">
            <label for="firstname">First name</label>
            <input v-model="firstname" type="text" id="firstname" name="firstname" placeholder="Enter your first name" required>
            <label for="lastname">Last name</label>
            <input v-model="lastname" type="text" id="lastname" name="lastname" placeholder="Enter your last name" required>
            <label for="address">Address</label>
            <input v-model="address" type="text" id="address" name="address" placeholder="Enter your address" required>
            <label for="city">City</label>
            <input v-model="city" type="text" id="city" name="city" placeholder="Enter your city" required>
            <label for="email">Email</label>
            <input v-model="email" type="email" id="email" name="email" placeholder="Enter your email" required>
            <label for="phone">Phone nr</label>
            <input v-model="phone" type="text" id="phone" name="phone" placeholder="Enter your phone number" required>
            <label for="createpassword">Password</label>
            <input v-model="password" type="password" id="createpassword" name="password" placeholder="Create your password" required>
            <button type="submit" @click="newRegistration()">Register</button>
        </form>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';
 
    export default defineComponent({
        name: 'NewRegister',
        components: {

        },
        props: {
        },
        data() {
            return {
                firstname: '',
                lastname: '',
                address: '',
                city: '',
                email: '',
                password: '',
                phone: ''
            }
        },
        computed: {
        },
        watch: {
        },
        methods: {
            async newRegistration() {
                try {
                    const response = await fetch('https://localhost:7218/api/Registration', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({
                            firstname: this.firstname,
                            lastname: this.lastname,
                            address: this.address,
                            city: this.city,
                            email: this.email,
                            password: this.password,
                            phone: this.phone
                        })
                    });

                   

                    if (response.ok) {
                         const data = await response.json();
                         console.log(data);
                        alert('New user added');
                        this.closeModal();
                    } else {
                        if (response.status === 409) {
                            alert('A user with this email already exists.');
                        } else {
                            alert('Something went wrong!');
                        }
                    }
                } catch (error) {
                    console.error('Error:', error);
                }
            },
            closeModal() {
                this.$emit('close');
            }
        }

    });
</script>

<style scoped>

    .modal {
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        padding: 10px 10px 10px 10px;
        width: 75%;
        height: 75%;
        overflow: auto;
        border-radius: 0.7rem;
        background: linear-gradient(to bottom left,darkorange,yellow);
    }

    .close-button {
        position: absolute;
        top: 10px;
        right: 10px;
    }
    .form {
        display:grid;
        gap: 1rem;
    }

    label {
        font-weight: bold;
        color: #333;
    }

    input {
        padding: 0.5rem;
        border: 1px solid #ccc;
        border-radius: 0.5rem;
    }

    button {
        padding: 0.5rem 1rem;
        background-color: #333;
        color: white;
        border: none;
        border-radius: 0.5rem;
        cursor: pointer;
    }

        button:hover {
            background-color: #555;
        }

        button:active {
            background-color: #777;
        }

        button:focus {
            outline: none;
        }
    
</style>
