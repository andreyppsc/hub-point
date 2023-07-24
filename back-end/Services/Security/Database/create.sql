create schema sec;

create table sec.permissions (
    permission_id           uuid not null,
    permission_name         varchar(50) not null,

    -- pk --
    constraint pk_permissions primary key (permission_id),
    -- ix --
    constraint ix_permissions unique (permission_name)
);

create table sec.roles (
    role_id                 uuid not null,
    name                    varchar(50) not null,

    -- pk --
    constraint pk_roles primary key (role_id),
    -- ix --
    constraint ix_roles unique (name)
);

create table sec.roles_permissions (
    role_id                 uuid not null,
    permission_id           uuid not null,

    -- pk --
    constraint pk_roles_permissions primary key (role_id, permission_id),
    -- fk --
    constraint fk_roles_permissions_roles foreign key (role_id) references sec.roles (role_id),
    constraint fk_roles_permissions_permissions foreign key (permission_id) references sec.permissions (permission_id)
);

create table sec.users (
    user_id                 uuid not null,
    user_name               varchar(50) not null,
    email                   varchar(50) not null,
    first_name              varchar(50) not null,
    last_name               varchar(50) not null,

    -- pk --
    constraint pk_users primary key (user_id),
    -- ix --
    constraint ix_users unique (user_name)
);

create table sec.users_roles (
    user_id                 uuid not null,
    role_id                 uuid not null,
    
    -- pk --
    constraint pk_users_roles primary key (user_id, role_id),
    -- fk --
    constraint fk_users_roles_users foreign key (user_id) references sec.users (user_id),
    constraint fk_users_roles_roles foreign key (role_id) references sec.roles (role_id)
);