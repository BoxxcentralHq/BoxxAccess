"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import { HugeiconsIcon } from "@hugeicons/react";
import {
  AlarmClockIcon,
  ArrowLeft01Icon,
  ArrowRight01Icon,
  Activity01Icon,
  Calendar01Icon,
  CircuitBoardIcon,
  CreditCardAcceptIcon,
  Door01Icon,
  File01Icon,
  Location01Icon,
  LockKeyholeIcon,
  Shield01Icon,
  SmartPhone01Icon,
  Time01Icon,
} from "@hugeicons/core-free-icons";
import {
  Collapsible,
  CollapsibleContent,
  CollapsibleTrigger,
} from "@/components/ui/collapsible";
import {
  Sidebar,
  SidebarContent,
  SidebarGroup,
  SidebarGroupContent,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarMenuSub,
  SidebarMenuSubButton,
  SidebarMenuSubItem,
} from "@/components/ui/sidebar";

const navGroups = [
  {
    label: "Access Device",
    icon: LockKeyholeIcon,
    items: [
      { href: "/access-device/device", label: "Device", icon: SmartPhone01Icon },
      { href: "/access-device/io-board", label: "I/O Board", icon: CircuitBoardIcon },
      { href: "/access-device/door", label: "Door", icon: Door01Icon },
      { href: "/access-device/reader", label: "Reader", icon: CreditCardAcceptIcon },
      { href: "/access-device/auxiliary-input", label: "Auxiliary Input", icon: ArrowRight01Icon },
      { href: "/access-device/auxiliary-output", label: "Auxiliary Output", icon: ArrowLeft01Icon },
      { href: "/access-device/event-type", label: "Event Type", icon: Calendar01Icon },
      { href: "/access-device/daylight-saving-time", label: "Daylight Saving Time", icon: Time01Icon },
      { href: "/access-device/real-time-monitoring", label: "Real-Time Monitoring", icon: Activity01Icon },
      { href: "/access-device/alarm-monitoring", label: "Alarm Monitoring", icon: AlarmClockIcon },
      { href: "/access-device/map", label: "Map", icon: Location01Icon },
    ],
  },
  {
    label: "Access Rule",
    icon: Shield01Icon,
    items: [] as { href: string; label: string; icon: typeof Shield01Icon }[],
  },
  {
    label: "Access Control Reports",
    icon: File01Icon,
    items: [] as { href: string; label: string; icon: typeof File01Icon }[],
  },
];

export default function AppSidebar() {
  const pathname = usePathname();

  return (
    <Sidebar>
      <SidebarContent>
        <SidebarGroup>
          <SidebarGroupContent>
            <SidebarMenu>
              {navGroups.map((group) => {
                const isGroupActive = group.items.some((item) => item.href === pathname);

                return (
                  <Collapsible key={group.label} defaultOpen={isGroupActive}>
                    <SidebarMenuItem>
                      <CollapsibleTrigger asChild>
                        <SidebarMenuButton isActive={isGroupActive}>
                          <HugeiconsIcon icon={group.icon} size={18} />
                          <span>{group.label}</span>
                        </SidebarMenuButton>
                      </CollapsibleTrigger>
                      {group.items.length > 0 && (
                        <CollapsibleContent>
                          <SidebarMenuSub>
                            {group.items.map((item) => (
                              <SidebarMenuSubItem key={item.href}>
                                <SidebarMenuSubButton asChild isActive={pathname === item.href}>
                                  <Link href={item.href}>
                                    <HugeiconsIcon icon={item.icon} size={16} />
                                    <span>{item.label}</span>
                                  </Link>
                                </SidebarMenuSubButton>
                              </SidebarMenuSubItem>
                            ))}
                          </SidebarMenuSub>
                        </CollapsibleContent>
                      )}
                    </SidebarMenuItem>
                  </Collapsible>
                );
              })}
            </SidebarMenu>
          </SidebarGroupContent>
        </SidebarGroup>
      </SidebarContent>
    </Sidebar>
  );
}
